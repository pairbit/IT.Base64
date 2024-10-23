using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace IT.Base64.Internal;

internal static class Vector128Base64Url
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsValid96(ref byte encoded)
    {
        if (Ssse3.IsSupported)
        {
            if (!IsSsse3Valid128(Vector128.LoadUnsafe(ref encoded).AsSByte()))
                return false;
        }
        else
        {
            if (!IsArm64Valid128(Vector128.LoadUnsafe(ref encoded).AsSByte()))
                return false;
        }
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool IsValid96(ref char encoded)
    {
        if (Ssse3.IsSupported)
        {
            if (!IsSsse3Valid128(xSse2.LoadUnsafe(ref encoded).AsSByte()))
                return false;
        }
        else
        {
            if (!IsArm64Valid128(xArm64.LoadUnsafe(ref encoded).AsSByte()))
                return false;
        }
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool TryDecode96(ref byte encoded, ref byte src)
    {
        Vector128<sbyte> vector = Vector128.LoadUnsafe(ref encoded).AsSByte();
        Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
        Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
        Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
        if (Ssse3.IsSupported)
        {
            if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                return false;
            vector = Ssse3Decode128(vector, hiNibbles, eq5F);
        }
        else
        {
            if (!IsArm64Valid128(vector, hiNibbles, eq5F))
                return false;
            vector = Arm64Decode128(vector, hiNibbles, eq5F);
        }
        vector.AsByte().StoreUnsafe(ref src);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static bool TryDecode96(ref char encoded, ref byte src)
    {
        Vector128<sbyte> vector;
        Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
        if (Ssse3.IsSupported)
        {
            vector = xSse2.LoadUnsafe(ref encoded).AsSByte();
            Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
            Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
            if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                return false;
            vector = Ssse3Decode128(vector, hiNibbles, eq5F);
        }
        else
        {
            vector = xArm64.LoadUnsafe(ref encoded).AsSByte();
            Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
            Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
            if (!IsArm64Valid128(vector, hiNibbles, eq5F))
                return false;
            vector = Arm64Decode128(vector, hiNibbles, eq5F);
        }
        vector.AsByte().StoreUnsafe(ref src);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Encode96(ref byte src, ref byte encoded)
        => (Ssse3.IsSupported ? Ssse3Encode128(ref src) : Arm64Encode128(ref src)).AsByte().StoreUnsafe(ref encoded);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void Encode96(ref byte src, ref char encoded)
    {
        if (Ssse3.IsSupported)
        {
            xSse2.StoreUnsafe(Ssse3Encode128(ref src), ref encoded);
        }
        else
        {
            xVector128.StoreUnsafe(Arm64Encode128(ref src), ref encoded);
        }
    }

    #region Private

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSsse3Valid128(Vector128<sbyte> vector)
    {
        Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
        Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
        Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
        return IsSsse3Valid128(vector, hiNibbles, eq5F);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsArm64Valid128(Vector128<sbyte> vector)
    {
        Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
        Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
        Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
        return IsArm64Valid128(vector, hiNibbles, eq5F);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsSsse3Valid128(Vector128<sbyte> vector, Vector128<sbyte> hiNibbles, Vector128<sbyte> eq5F)
    {
        // Take care as arguments are flipped in order!
        //Vector128<sbyte> outside = Sse2.AndNot(eq5F, below | above);
        return Vector128.AndNot(
            Vector128.LessThan(vector, Ssse3.Shuffle(Vector128.Create(
                1, 1, 0x2d, 0x30,
                0x41, 0x50, 0x61, 0x70,
                1, 1, 1, 1,
                1, 1, 1, 1
            ), hiNibbles)) |
            Vector128.GreaterThan(vector, Ssse3.Shuffle(Vector128.Create(
               0x00, 0x00, 0x2d, 0x39,
               0x4f, 0x5a, 0x6f, 0x7a,
               0x00, 0x00, 0x00, 0x00,
               0x00, 0x00, 0x00, 0x00
            ), hiNibbles)), eq5F) == default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsArm64Valid128(Vector128<sbyte> vector, Vector128<sbyte> hiNibbles, Vector128<sbyte> eq5F)
    {
        return Vector128.AndNot(
            Vector128.LessThan(vector, xArm64.Shuffle(Vector128.Create(
                1, 1, 0x2d, 0x30,
                0x41, 0x50, 0x61, 0x70,
                1, 1, 1, 1,
                1, 1, 1, 1
            ).AsByte(), hiNibbles)) |
            Vector128.GreaterThan(vector, xArm64.Shuffle(Vector128.Create(
               0x00, 0x00, 0x2d, 0x39,
               0x4f, 0x5a, 0x6f, 0x7a,
               0x00, 0x00, 0x00, 0x00,
               0x00, 0x00, 0x00, 0x00
            ).AsByte(), hiNibbles)), eq5F) == default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Ssse3Decode128(Vector128<sbyte> vector, Vector128<sbyte> hiNibbles, Vector128<sbyte> eq5F)
        => Ssse3.Shuffle(
            Sse2.MultiplyAddAdjacent(
                Ssse3.MultiplyAddAdjacent(
                    (vector +
                    Ssse3.Shuffle(GetDecodeLutShift128(), hiNibbles) +
                    (eq5F & Vector128.Create((sbyte)33))).AsByte(),
                    Vector128.Create(0x01400140).AsSByte()),
                Vector128.Create(0x00011000).AsInt16()).AsSByte(),
            GetDecodeShuffleVec());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Arm64Decode128(Vector128<sbyte> vector, Vector128<sbyte> hiNibbles, Vector128<sbyte> eq5F)
        => xArm64.Shuffle(
            xArm64.MultiplyAddAdjacent(
                xArm64.MultiplyAddAdjacent(
                    (vector +
                    xArm64.Shuffle(GetDecodeLutShift128().AsByte(), hiNibbles) +
                    (eq5F & Vector128.Create((sbyte)33))).AsByte()
                )
            ).AsByte(),
            GetDecodeShuffleVec());

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Ssse3Encode128(ref byte src)
    {
        Vector128<sbyte> vector = Ssse3.Shuffle(Vector128.LoadUnsafe(ref src).AsSByte(), GetEncodeShuffleVec());
        vector = Sse2.MultiplyHigh(
               (vector & Vector128.Create(0x0fc0fc00).AsSByte()).AsUInt16(), Vector128.Create(0x04000040).AsUInt16()).AsSByte() |
              ((vector & Vector128.Create(0x003f03f0).AsSByte()).AsInt16() * Vector128.Create(0x01000010).AsInt16()).AsSByte();
        vector += Ssse3.Shuffle(GetEncodeLut128(),
                Sse2.SubtractSaturate(vector.AsByte(), Vector128.Create((byte)51)).AsSByte() -
                Vector128.GreaterThan(vector, Vector128.Create((sbyte)25)));
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Arm64Encode128(ref byte src)
    {
        Vector128<sbyte> vector = xArm64.Shuffle(Vector128.LoadUnsafe(ref src), GetEncodeShuffleVec());
        vector = xArm64.MultiplyHigh(
               (vector & Vector128.Create(0x0fc0fc00).AsSByte()).AsUInt16()).AsSByte() |
              ((vector & Vector128.Create(0x003f03f0).AsSByte()).AsInt16() * Vector128.Create(0x01000010).AsInt16()).AsSByte();
        vector += xArm64.Shuffle(GetEncodeLut128().AsByte(),
                AdvSimd.SubtractSaturate(vector.AsByte(), Vector128.Create((byte)51)).AsSByte() -
                Vector128.GreaterThan(vector, Vector128.Create((sbyte)25)));
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetDecodeLutShift128() => Vector128.Create(
        0, 0, 17, 4,
      -65, -65, -71, -71,
        0, 0, 0, 0,
        0, 0, 0, 0
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetDecodeShuffleVec() => Vector128.Create(
        2, 1, 0, 6,
        5, 4, 10, 9,
        8, 14, 13, 12,
        -1, -1, -1, -1
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetEncodeLut128() => Vector128.Create(
       65, 71, -4, -4,
       -4, -4, -4, -4,
       -4, -4, -4, -4,
       -17, 32, 0, 0
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetEncodeShuffleVec() => Vector128.Create(
        1, 0, 2, 1,
        4, 3, 5, 4,
        7, 6, 8, 7,
        10, 9, 11, 10
    );

    #endregion Private
}