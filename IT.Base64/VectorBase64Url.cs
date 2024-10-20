using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace IT.Base64;

public static class VectorBase64Url
{
    #region Encode128

    public static EncodingStatus TryEncode128(Int128 value, Span<byte> encoded)
    {
        if (encoded.Length < 22) return EncodingStatus.InvalidDestinationLength;

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public static EncodingStatus TryEncode128(Int128 value, Span<char> encoded)
    {
        if (encoded.Length < 22) return EncodingStatus.InvalidDestinationLength;

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Encode128(Int128 value, Span<byte> encoded)
    {
        if (encoded.Length < 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 22");

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Encode128(Int128 value, Span<char> encoded)
    {
        if (encoded.Length < 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 22");

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    public static byte[] Encode128ToBytes(Int128 value)
    {
        var encoded = new byte[22];

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public static char[] Encode128ToChars(Int128 value)
    {
        var encoded = new char[22];

        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public static string Encode128ToString(Int128 value) => string.Create(22, value, static (chars, value) =>
    {
        Encode128(ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode128

    #region Decode128

    public static DecodingStatus TryDecode128(ReadOnlySpan<byte> encoded, out Int128 value)
    {
        value = default;
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;

        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public static DecodingStatus TryDecode128(ReadOnlySpan<char> encoded, out Int128 value)
    {
        value = default;
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;

        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value)))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public static DecodingStatus TryDecode128(ReadOnlySpan<byte> encoded, out Int128 value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    public static DecodingStatus TryDecode128(ReadOnlySpan<char> encoded, out Int128 value, out char invalid)
    {
        value = default;
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out invalid))
        {
            value = default;
            return DecodingStatus.InvalidData;
        }
        return DecodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static Int128 Decode128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");

        Int128 value = 0;
        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static Int128 Decode128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");

        Int128 value = 0;
        if (!TryDecode128(ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<Int128, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode128

    #region Valid128

    public static DecodingStatus TryValid128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;
        return IsValid128(ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public static DecodingStatus TryValid128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) return DecodingStatus.InvalidDataLength;
        return IsValid128(ref MemoryMarshal.GetReference(encoded)) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public static DecodingStatus TryValid128(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return IsValid128(ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    public static DecodingStatus TryValid128(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 22)
        {
            invalid = default;
            return DecodingStatus.InvalidDataLength;
        }
        return IsValid128(ref MemoryMarshal.GetReference(encoded), out invalid) ? DecodingStatus.Done : DecodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Valid128(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");
        if (!IsValid128(ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static void Valid128(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 22");
        if (!IsValid128(ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid128

    #region Unsafe

    public static void Encode128(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            (Ssse3.IsSupported ? Ssse3Encode128(ref src) : Arm64Encode128(ref src)).AsByte().StoreUnsafe(ref encoded);
            ref var bytes = ref MemoryMarshal.GetReference(Base64Encoder.Url.Bytes.Span);
            UnsafeBase64.Encode24(ref bytes, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
            UnsafeBase64.Encode8(ref bytes, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 20));
        }
        else
        {
            UnsafeBase64.Encode128(ref MemoryMarshal.GetReference(Base64Encoder.Url.Bytes.Span), ref src, ref encoded);
        }
    }

    public static void Encode128(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            if (Ssse3.IsSupported)
            {
                xSse2.StoreUnsafe(Ssse3Encode128(ref src), ref encoded);
            }
            else
            {
                xVector128.StoreUnsafe(Arm64Encode128(ref src), ref encoded);
            }
            ref var chars = ref MemoryMarshal.GetReference(Base64Encoder.Url.Chars.Span);
            UnsafeBase64.Encode24(ref chars, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
            UnsafeBase64.Encode8(ref chars, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 40));
        }
        else
        {
            UnsafeBase64.Encode128(ref MemoryMarshal.GetReference(Base64Encoder.Url.Chars.Span), ref src, ref encoded);
        }
    }

    public static bool TryDecode128(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128<sbyte> vector;
            if (Ssse3.IsSupported)
            {
                vector = Vector128.LoadUnsafe(ref encoded).AsSByte();
                Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
                Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
                Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
                if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                    return false;
                vector = Ssse3Decode128(vector, hiNibbles, eq5F);
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            vector.AsByte().StoreUnsafe(ref src);
            return UnsafeBase64.TryDecode32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, ref src);
        }
    }

    public static bool TryDecode128(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128<sbyte> vector;
            if (Ssse3.IsSupported)
            {
                vector = xSse2.LoadUnsafe(ref encoded).AsSByte();
                Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
                Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
                Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
                if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                    return false;
                vector = Ssse3Decode128(vector, hiNibbles, eq5F);
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            vector.AsByte().StoreUnsafe(ref src);
            return UnsafeBase64.TryDecode32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, ref src);
        }
    }

    public static bool TryDecode128(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128<sbyte> vector;
            if (Ssse3.IsSupported)
            {
                vector = Vector128.LoadUnsafe(ref encoded).AsSByte();
                Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
                Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
                Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
                if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                {
                    invalid = UnsafeBase64.GetInvalid(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, 16);
                    return false;
                }
                vector = Ssse3Decode128(vector, hiNibbles, eq5F);
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            vector.AsByte().StoreUnsafe(ref src);
            return UnsafeBase64.TryDecode32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode128(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128<sbyte> vector;
            if (Ssse3.IsSupported)
            {
                vector = xSse2.LoadUnsafe(ref encoded).AsSByte();
                Vector128<sbyte> maskSlashOrUnderscore = Vector128.Create((sbyte)0x5F);//_
                Vector128<sbyte> hiNibbles = Vector128.ShiftRightLogical(vector.AsInt32(), 4).AsSByte() & maskSlashOrUnderscore;
                Vector128<sbyte> eq5F = Vector128.Equals(vector, maskSlashOrUnderscore);
                if (!IsSsse3Valid128(vector, hiNibbles, eq5F))
                {
                    invalid = UnsafeBase64.GetInvalid(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, 32);
                    return false;
                }
                vector = Ssse3Decode128(vector, hiNibbles, eq5F);
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            vector.AsByte().StoreUnsafe(ref src);
            return UnsafeBase64.TryDecode32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, ref src, out invalid);
        }
    }

    public static bool IsValid128(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            if (Ssse3.IsSupported)
            {
                if (!IsSsse3Valid128(Vector128.LoadUnsafe(ref encoded).AsSByte()))
                    return false;
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            return UnsafeBase64.IsValid32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            return UnsafeBase64.IsValid128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded);
        }
    }

    public static bool IsValid128(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            if (Ssse3.IsSupported)
            {
                if (!IsSsse3Valid128(xSse2.LoadUnsafe(ref encoded).AsSByte()))
                    return false;
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            return UnsafeBase64.IsValid32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            return UnsafeBase64.IsValid128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded);
        }
    }

    public static bool IsValid128(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            if (Ssse3.IsSupported)
            {
                if (!IsSsse3Valid128(Vector128.LoadUnsafe(ref encoded).AsSByte()))
                {
                    invalid = UnsafeBase64.GetInvalid(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, 16);
                    return false;
                }
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            return UnsafeBase64.IsValid32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, out invalid);
        }
    }

    public static bool IsValid128(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            if (Ssse3.IsSupported)
            {
                if (!IsSsse3Valid128(xSse2.LoadUnsafe(ref encoded).AsSByte()))
                {
                    invalid = UnsafeBase64.GetInvalid(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, 32);
                    return false;
                }
            }
            else
            {
                throw new NotImplementedException("Arm64");
            }
            return UnsafeBase64.IsValid32(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid128(ref MemoryMarshal.GetReference(Base64Decoder.Url.Map.Span), ref encoded, out invalid);
        }
    }

    #endregion Unsafe

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
    private static Vector128<sbyte> Ssse3Decode128(Vector128<sbyte> vector, Vector128<sbyte> hiNibbles, Vector128<sbyte> eq5F)
        => Ssse3.Shuffle(
            Sse2.MultiplyAddAdjacent(
                Ssse3.MultiplyAddAdjacent(
                    (vector + Ssse3.Shuffle(Vector128.Create(
                        0, 0, 17, 4,
                        -65, -65, -71, -71,
                        0, 0, 0, 0,
                        0, 0, 0, 0
                    ), hiNibbles) + (eq5F & Vector128.Create((sbyte)33))).AsByte(),
                    Vector128.Create(0x01400140).AsSByte()),
                Vector128.Create(0x00011000).AsInt16()).AsSByte(),
            Vector128.Create(
                2, 1, 0, 6,
                5, 4, 10, 9,
                8, 14, 13, 12,
                -1, -1, -1, -1
            ));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Ssse3Encode128(ref byte src)
    {
        Vector128<sbyte> vector = Ssse3.Shuffle(Vector128.LoadUnsafe(ref src).AsSByte(), GetShuffleVec());
        vector = Sse2.MultiplyHigh(
               (vector & Vector128.Create(0x0fc0fc00).AsSByte()).AsUInt16(), Vector128.Create(0x04000040).AsUInt16()).AsSByte() |
              ((vector & Vector128.Create(0x003f03f0).AsSByte()).AsInt16() * Vector128.Create(0x01000010).AsInt16()).AsSByte();
        vector += Ssse3.Shuffle(GetLut128(),
                Sse2.SubtractSaturate(vector.AsByte(), Vector128.Create((byte)51)).AsSByte() -
                Vector128.GreaterThan(vector, Vector128.Create((sbyte)25)));
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> Arm64Encode128(ref byte src)
    {
        Vector128<sbyte> vector = xArm64.Shuffle(Vector128.LoadUnsafe(ref src), GetShuffleVec());
        vector = xArm64.MultiplyHigh(
               (vector & Vector128.Create(0x0fc0fc00).AsSByte()).AsUInt16()).AsSByte() |
              ((vector & Vector128.Create(0x003f03f0).AsSByte()).AsInt16() * Vector128.Create(0x01000010).AsInt16()).AsSByte();
        vector += xArm64.Shuffle(GetLut128().AsByte(),
                AdvSimd.SubtractSaturate(vector.AsByte(), Vector128.Create((byte)51)).AsSByte() -
                Vector128.GreaterThan(vector, Vector128.Create((sbyte)25)));
        return vector;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetLut128() => Vector128.Create(
       65, 71, -4, -4,
       -4, -4, -4, -4,
       -4, -4, -4, -4,
       -17, 32, 0, 0
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Vector128<sbyte> GetShuffleVec() => Vector128.Create(
        1, 0, 2, 1,
        4, 3, 5, 4,
        7, 6, 8, 7,
        10, 9, 11, 10
    );

    #endregion Private
}