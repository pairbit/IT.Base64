﻿using System;
using System.Runtime.CompilerServices;

namespace IT.Base64;

public static class UnsafeBase64
{
    #region Encode128

    public static void Encode128(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 20));
    }

    public static void Encode128(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 40));
    }

    #endregion Encode128

    #region Decode128

    public static bool TryDecode128(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 20), ref Unsafe.AddByteOffset(ref src, 15));

    public static bool TryDecode128(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 40), ref Unsafe.AddByteOffset(ref src, 15));

    public static bool TryDecode128(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 20), ref Unsafe.AddByteOffset(ref src, 15), out invalid);

    public static bool TryDecode128(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 40), ref Unsafe.AddByteOffset(ref src, 15), out invalid);

    #endregion Decode128

    #region IsValid128

    public static bool IsValid128(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 20));

    public static bool IsValid128(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 40));

    public static bool IsValid128(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 20), out invalid);

    public static bool IsValid128(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 40), out invalid);

    #endregion IsValid128

    #region Encode120

    public static void Encode120(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    public static void Encode120(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
    }

    #endregion Encode120

    #region Decode120

    public static bool TryDecode120(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode120(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode120(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    public static bool TryDecode120(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    #endregion Decode120

    #region IsValid120

    public static bool IsValid120(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid120(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32));

    public static bool IsValid120(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    public static bool IsValid120(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);

    #endregion IsValid120

    #region Encode112

    public static void Encode112(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    public static void Encode112(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
    }

    #endregion Encode112

    #region Decode112

    public static bool TryDecode112(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode112(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode112(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    public static bool TryDecode112(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    #endregion Decode112

    #region IsValid112

    public static bool IsValid112(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid112(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 32));

    public static bool IsValid112(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    public static bool IsValid112(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);

    #endregion IsValid112

    #region Encode104

    public static void Encode104(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    public static void Encode104(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
    }

    #endregion Encode104

    #region Decode104

    public static bool TryDecode104(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode104(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));

    public static bool TryDecode104(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    public static bool TryDecode104(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);

    #endregion Decode104

    #region IsValid104

    public static bool IsValid104(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid104(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 32));

    public static bool IsValid104(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    public static bool IsValid104(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);

    #endregion IsValid104

    #region Encode96

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Int128 Encode96ToInt128(ref byte map, ref byte src)
        => throw new NotImplementedException();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Guid Encode96ToGuid(ref byte map, ref byte src)
    {
        throw new NotImplementedException();
    }

    public static void Encode96(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
    }

    public static void Encode96(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
    }

    #endregion Encode96

    #region Decode96

    public static bool TryDecode96(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode96(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode96(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    public static bool TryDecode96(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    #endregion Decode96

    #region IsValid96

    public static bool IsValid96(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12));

    public static bool IsValid96(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24));

    public static bool IsValid96(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid);

    public static bool IsValid96(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid);

    #endregion IsValid96

    #region Encode88

    public static void Encode88(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
    }

    public static void Encode88(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
    }

    #endregion Encode88

    #region Decode88

    public static bool TryDecode88(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode88(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode88(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    public static bool TryDecode88(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    #endregion Decode88

    #region IsValid88

    public static bool IsValid88(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 12));

    public static bool IsValid88(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 24));

    public static bool IsValid88(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid);

    public static bool IsValid88(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid);

    #endregion IsValid88

    #region Encode80

    public static void Encode80(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 12));
    }

    public static void Encode80(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 9), ref Unsafe.AddByteOffset(ref encoded, 24));
    }

    #endregion Encode80

    #region Decode80

    public static bool TryDecode80(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode80(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9));

    public static bool TryDecode80(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    public static bool TryDecode80(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), ref Unsafe.AddByteOffset(ref src, 9), out invalid);

    #endregion Decode80

    #region IsValid80

    public static bool IsValid80(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 12));

    public static bool IsValid80(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 24));

    public static bool IsValid80(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 12), out invalid);

    public static bool IsValid80(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 24), out invalid);

    #endregion IsValid80

    #region Encode72

    public static void Encode72(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    public static void Encode72(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    #endregion Encode72

    #region Decode72

    public static bool TryDecode72(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode72(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode72(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    public static bool TryDecode72(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    #endregion Decode72

    #region IsValid72

    public static bool IsValid72(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid72(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid72(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    public static bool IsValid72(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    #endregion IsValid72

    #region Encode64

    public static void Encode64(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    public static void Encode64(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    #endregion Encode64

    #region Decode64

    public static bool TryDecode64(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode64(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode64(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    public static bool TryDecode64(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    #endregion Decode64

    #region IsValid64

    public static bool IsValid64(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid64(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid64(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    public static bool IsValid64(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    #endregion IsValid64

    #region Encode56

    public static void Encode56(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    public static void Encode56(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 6), ref Unsafe.AddByteOffset(ref encoded, 16));
    }

    #endregion Encode56

    #region Decode56

    public static bool TryDecode56(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode56(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3)) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6));

    public static bool TryDecode56(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    public static bool TryDecode56(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 6), out invalid);

    #endregion Decode56

    #region IsValid56

    public static bool IsValid56(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid56(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8)) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16));

    public static bool IsValid56(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    public static bool IsValid56(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);

    #endregion IsValid56

    #region Encode48

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Encode48ToInt64(ref byte map, ref byte src)
    {
        throw new NotImplementedException();
    }

    public static void Encode48(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
    }

    public static void Encode48(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode24(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    #endregion Encode48

    #region Decode48

    public static bool TryDecode48(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode48(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode48(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    public static bool TryDecode48(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    #endregion Decode48

    #region IsValid48

    public static bool IsValid48(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4));

    public static bool IsValid48(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid48(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid);

    public static bool IsValid48(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid24(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    #endregion IsValid48

    #region Encode40

    public static void Encode40(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
    }

    public static void Encode40(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode16(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    #endregion Encode40

    #region Decode40

    public static bool TryDecode40(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode40(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode40(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    public static bool TryDecode40(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    #endregion Decode40

    #region IsValid40

    public static bool IsValid40(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 4));

    public static bool IsValid40(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid40(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid);

    public static bool IsValid40(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid16(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    #endregion IsValid40

    #region Encode32

    public static void Encode32(ref byte map, ref byte src, ref byte encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 4));
    }

    public static void Encode32(ref char map, ref byte src, ref char encoded)
    {
        Encode24(ref map, ref src, ref encoded);
        Encode8(ref map, ref Unsafe.AddByteOffset(ref src, 3), ref Unsafe.AddByteOffset(ref encoded, 8));
    }

    #endregion Encode32

    #region Decode32

    public static bool TryDecode32(ref sbyte map, ref byte encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode32(ref sbyte map, ref char encoded, ref byte src) =>
        TryDecode24(ref map, ref encoded, ref src) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3));

    public static bool TryDecode32(ref sbyte map, ref byte encoded, ref byte src, out byte invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    public static bool TryDecode32(ref sbyte map, ref char encoded, ref byte src, out char invalid) =>
        TryDecode24(ref map, ref encoded, ref src, out invalid) &&
        TryDecode8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), ref Unsafe.AddByteOffset(ref src, 3), out invalid);

    #endregion Decode32

    #region IsValid32

    public static bool IsValid32(ref sbyte map, ref byte encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 4));

    public static bool IsValid32(ref sbyte map, ref char encoded) =>
        IsValid24(ref map, ref encoded) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8));

    public static bool IsValid32(ref sbyte map, ref byte encoded, out byte invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 4), out invalid);

    public static bool IsValid32(ref sbyte map, ref char encoded, out char invalid) =>
        IsValid24(ref map, ref encoded, out invalid) &&
        IsValid8(ref map, ref Unsafe.AddByteOffset(ref encoded, 8), out invalid);

    #endregion IsValid32

    #region Encode24

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Encode24ToInt32(ref byte map, ref byte src)
    {
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8 | Unsafe.AddByteOffset(ref src, 2);
        return Unsafe.AddByteOffset(ref map, i >> 18) |
               Unsafe.AddByteOffset(ref map, i >> 12 & 0x3F) << 8 |
               Unsafe.AddByteOffset(ref map, i >> 6 & 0x3F) << 16 |
               Unsafe.AddByteOffset(ref map, i & 0x3F) << 24;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode24(ref byte map, ref byte src, ref byte encoded)
    {
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8 | Unsafe.AddByteOffset(ref src, 2);
        encoded = Unsafe.AddByteOffset(ref map, i >> 18);
        Unsafe.AddByteOffset(ref encoded, 1) = Unsafe.AddByteOffset(ref map, i >> 12 & 0x3F);
        Unsafe.AddByteOffset(ref encoded, 2) = Unsafe.AddByteOffset(ref map, i >> 6 & 0x3F);
        Unsafe.AddByteOffset(ref encoded, 3) = Unsafe.AddByteOffset(ref map, i & 0x3F);

        //slowly
        //Unsafe.As<byte, int>(ref encoded) = map[i >> 18] | map[i >> 12 & 0x3F] << 8 | map[i >> 6 & 0x3F] << 16 | map[i & 0x3F] << 24;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode24(ref char map, ref byte src, ref char encoded)
    {
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8 | Unsafe.AddByteOffset(ref src, 2);
        encoded = Unsafe.AddByteOffset(ref map, i >> 18 << 1);
        Unsafe.AddByteOffset(ref encoded, 2) = Unsafe.AddByteOffset(ref map, (i >> 12 & 0x3F) << 1);
        Unsafe.AddByteOffset(ref encoded, 4) = Unsafe.AddByteOffset(ref map, (i >> 6 & 0x3F) << 1);
        Unsafe.AddByteOffset(ref encoded, 6) = Unsafe.AddByteOffset(ref map, (i & 0x3F) << 1);
    }

    #endregion Encode24

    #region Decode24

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode24(ref sbyte map, ref byte encoded, ref byte src)
    {
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12 |
                  Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 2)) << 6 |
                  (int)Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 3));
        if (val < 0) return false;

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        Unsafe.AddByteOffset(ref src, 2) = (byte)val;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode24(ref sbyte map, ref char encoded, ref byte src)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        var i3 = Unsafe.AddByteOffset(ref encoded, 6);
        if (((encoded | i1 | i2 | i3) & 0xffffff00) != 0) return false;

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6 |
                  (int)Unsafe.AddByteOffset(ref map, i3);
        if (val < 0) return false;

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        Unsafe.AddByteOffset(ref src, 2) = (byte)val;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode24(ref sbyte map, ref byte encoded, ref byte src, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        var i2 = Unsafe.AddByteOffset(ref encoded, 2);
        var i3 = Unsafe.AddByteOffset(ref encoded, 3);
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6 |
                  (int)Unsafe.AddByteOffset(ref map, i3);
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : Unsafe.AddByteOffset(ref map, i2) < 0 ? i2 : i3;
            return false;
        }

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        Unsafe.AddByteOffset(ref src, 2) = (byte)val;
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode24(ref sbyte map, ref char encoded, ref byte src, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        var i3 = Unsafe.AddByteOffset(ref encoded, 6);
        if (((encoded | i1 | i2 | i3) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1 > 255 ? i1 : i2 > 255 ? i2 : i3;
            return false;
        }

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6 |
                  (int)Unsafe.AddByteOffset(ref map, i3);
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : Unsafe.AddByteOffset(ref map, i2) < 0 ? i2 : i3;
            return false;
        }

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        Unsafe.AddByteOffset(ref src, 2) = (byte)val;
        invalid = default;
        return true;
    }

    #endregion Decode24

    #region IsValid24

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid24(ref sbyte map, ref byte encoded)
        => (Unsafe.AddByteOffset(ref map, encoded) << 18 |
            Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12 |
            Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 2)) << 6 |
            (int)Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 3))) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid24(ref sbyte map, ref char encoded)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        var i3 = Unsafe.AddByteOffset(ref encoded, 6);
        if (((encoded | i1 | i2 | i3) & 0xffffff00) != 0) return false;
        return (Unsafe.AddByteOffset(ref map, encoded) << 18 |
                Unsafe.AddByteOffset(ref map, i1) << 12 |
                Unsafe.AddByteOffset(ref map, i2) << 6 |
                (int)Unsafe.AddByteOffset(ref map, i3)) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid24(ref sbyte map, ref byte encoded, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        var i2 = Unsafe.AddByteOffset(ref encoded, 2);
        var i3 = Unsafe.AddByteOffset(ref encoded, 3);
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 |
            Unsafe.AddByteOffset(ref map, i1) << 12 |
            Unsafe.AddByteOffset(ref map, i2) << 6 |
            (int)Unsafe.AddByteOffset(ref map, i3)) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : Unsafe.AddByteOffset(ref map, i2) < 0 ? i2 : i3;
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid24(ref sbyte map, ref char encoded, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        var i3 = Unsafe.AddByteOffset(ref encoded, 6);
        if (((encoded | i1 | i2 | i3) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1 > 255 ? i1 : i2 > 255 ? i2 : i3;
            return false;
        }
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 |
            Unsafe.AddByteOffset(ref map, i1) << 12 |
            Unsafe.AddByteOffset(ref map, i2) << 6 |
            (int)Unsafe.AddByteOffset(ref map, i3)) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : Unsafe.AddByteOffset(ref map, i2) < 0 ? i2 : i3;
            return false;
        }
        invalid = default;
        return true;
    }

    #endregion IsValid24

    #region Encode16

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode16(ref byte map, ref byte src, ref byte encoded)
    {
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8;
        encoded = Unsafe.AddByteOffset(ref map, i >> 18);
        Unsafe.AddByteOffset(ref encoded, 1) = Unsafe.AddByteOffset(ref map, i >> 12 & 0x3F);
        Unsafe.AddByteOffset(ref encoded, 2) = Unsafe.AddByteOffset(ref map, i >> 6 & 0x3F);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode16(ref char map, ref byte src, ref char encoded)
    {
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8;
        encoded = Unsafe.AddByteOffset(ref map, i >> 18 << 1);
        Unsafe.AddByteOffset(ref encoded, 2) = Unsafe.AddByteOffset(ref map, (i >> 12 & 0x3F) << 1);
        Unsafe.AddByteOffset(ref encoded, 4) = Unsafe.AddByteOffset(ref map, (i >> 6 & 0x3F) << 1);
    }

    #endregion Encode16

    #region Decode16

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode16(ref sbyte map, ref byte encoded, ref byte src)
    {
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12 |
                  Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 2)) << 6;
        if (val < 0) return false;

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode16(ref sbyte map, ref char encoded, ref byte src)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        if (((encoded | i1 | i2) & 0xffffff00) != 0) return false;

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6;
        if (val < 0) return false;

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode16(ref sbyte map, ref byte encoded, ref byte src, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        var i2 = Unsafe.AddByteOffset(ref encoded, 2);
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6;
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : i2;
            return false;
        }

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode16(ref sbyte map, ref char encoded, ref byte src, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        if (((encoded | i1 | i2) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1 > 255 ? i1 : i2;
            return false;
        }

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, i1) << 12 |
                  Unsafe.AddByteOffset(ref map, i2) << 6;
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : i2;
            return false;
        }

        src = (byte)(val >> 16);
        Unsafe.AddByteOffset(ref src, 1) = (byte)(val >> 8);
        invalid = default;
        return true;
    }

    #endregion Decode16

    #region IsValid16

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid16(ref sbyte map, ref byte encoded)
        => (Unsafe.AddByteOffset(ref map, encoded) << 18 |
            Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12 |
            Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 2)) << 6) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid16(ref sbyte map, ref char encoded)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        if (((encoded | i1 | i2) & 0xffffff00) != 0) return false;

        return (Unsafe.AddByteOffset(ref map, encoded) << 18 |
                Unsafe.AddByteOffset(ref map, i1) << 12 |
                Unsafe.AddByteOffset(ref map, i2) << 6) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid16(ref sbyte map, ref byte encoded, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        var i2 = Unsafe.AddByteOffset(ref encoded, 2);
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 |
             Unsafe.AddByteOffset(ref map, i1) << 12 |
             Unsafe.AddByteOffset(ref map, i2) << 6) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : i2;
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid16(ref sbyte map, ref char encoded, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        var i2 = Unsafe.AddByteOffset(ref encoded, 4);
        if (((encoded | i1 | i2) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1 > 255 ? i1 : i2;
            return false;
        }
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 |
             Unsafe.AddByteOffset(ref map, i1) << 12 |
             Unsafe.AddByteOffset(ref map, i2) << 6) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : Unsafe.AddByteOffset(ref map, i1) < 0 ? i1 : i2;
            return false;
        }
        invalid = default;
        return true;
    }

    #endregion IsValid16

    #region Encode8

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Encode8ToInt16(ref byte map, ref byte src)
    {
        int i = src << 8;
        return (short)(Unsafe.AddByteOffset(ref map, i >> 10) | Unsafe.AddByteOffset(ref map, i >> 4 & 0x3F) << 8);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode8(ref byte map, ref byte src, ref byte encoded)
    {
        int i = src << 8;
        encoded = Unsafe.AddByteOffset(ref map, i >> 10);
        Unsafe.AddByteOffset(ref encoded, 1) = Unsafe.AddByteOffset(ref map, i >> 4 & 0x3F);

        //slowly
        //encoded = map[(src & 0xfc) >> 2];
        //Unsafe.AddByteOffset(ref encoded, 1) = map[(src & 0x03) << 4];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Encode8(ref char map, ref byte src, ref char encoded)
    {
        int i = src << 8;
        encoded = Unsafe.AddByteOffset(ref map, i >> 10 << 1);
        Unsafe.AddByteOffset(ref encoded, 2) = Unsafe.AddByteOffset(ref map, (i >> 4 & 0x3F) << 1);

        //encoded = map[(src & 0xfc) >> 2];
        //Unsafe.AddByteOffset(ref encoded, 2) = map[(src & 0x03) << 4];
    }

    #endregion Encode8

    #region Decode8

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode8(ref sbyte map, ref byte encoded, ref byte src)
    {
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 |
                  Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12;
        if (val < 0) return false;

        src = (byte)(val >> 16);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode8(ref sbyte map, ref char encoded, ref byte src)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        if (((encoded | i1) & 0xffffff00) != 0) return false;

        //TODO: Testing ???
        //if ((encoded | i1) > 255) return false;

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12;
        if (val < 0) return false;

        src = (byte)(val >> 16);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode8(ref sbyte map, ref byte encoded, ref byte src, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12;
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : i1;
            return false;
        }

        src = (byte)(val >> 16);
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryDecode8(ref sbyte map, ref char encoded, ref byte src, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        if (((encoded | i1) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1;
            return false;
        }

        var val = Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12;
        if (val < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : i1;
            return false;
        }

        src = (byte)(val >> 16);
        invalid = default;
        return true;
    }

    #endregion Decode8

    #region IsValid8

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid8(ref sbyte map, ref byte encoded)
        => (Unsafe.AddByteOffset(ref map, encoded) << 18 |
            Unsafe.AddByteOffset(ref map, Unsafe.AddByteOffset(ref encoded, 1)) << 12) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid8(ref sbyte map, ref char encoded)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        if (((encoded | i1) & 0xffffff00) != 0) return false;

        //TODO: Testing ???
        //if ((encoded | i1) > 255) return false;

        return (Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12) >= 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid8(ref sbyte map, ref byte encoded, out byte invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 1);
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : i1;
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsValid8(ref sbyte map, ref char encoded, out char invalid)
    {
        var i1 = Unsafe.AddByteOffset(ref encoded, 2);
        if (((encoded | i1) & 0xffffff00) != 0)
        {
            invalid = encoded > 255 ? encoded : i1;
            return false;
        }
        if ((Unsafe.AddByteOffset(ref map, encoded) << 18 | Unsafe.AddByteOffset(ref map, i1) << 12) < 0)
        {
            invalid = Unsafe.AddByteOffset(ref map, encoded) < 0 ? encoded : i1;
            return false;
        }
        invalid = default;
        return true;
    }

    #endregion IsValid8

    public static byte GetInvalid(ref sbyte map, ref byte encoded, int len)
    {
        for (int i = 0; i < len; i++)
        {
            var invalid = Unsafe.AddByteOffset(ref encoded, i);
            if (Unsafe.AddByteOffset(ref map, invalid) < 0) return invalid;
        }
        throw new InvalidOperationException("invalid byte not found");
    }

    public static char GetInvalid(ref sbyte map, ref char encoded, int len)
    {
        for (int i = 0; i < len; i += 2)
        {
            var invalid = Unsafe.AddByteOffset(ref encoded, i);
            if (invalid > 255 || Unsafe.AddByteOffset(ref map, invalid) < 0) return invalid;
        }
        throw new InvalidOperationException("invalid char not found");
    }
}