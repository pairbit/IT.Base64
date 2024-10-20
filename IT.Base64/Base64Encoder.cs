using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Base64;

public class Base64Encoder
{
    public static readonly Base64Encoder Default = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");
    public static readonly Base64Encoder Url = new("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_");

    public const int MaxDataLength = int.MaxValue / 4 * 3; // 1610612733

    internal readonly char[] _chars;
    internal readonly byte[] _bytes;

    public string Encoding { get; }

    public ReadOnlyMemory<char> Chars => _chars;

    public ReadOnlyMemory<byte> Bytes => _bytes;

    public Base64Encoder(string encoding)
    {
        Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));

        _chars = encoding.ToCharArray();
        _bytes = System.Text.Encoding.UTF8.GetBytes(Encoding);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetMaxEncodedLength(int dataLength)
    {
        if ((uint)dataLength > MaxDataLength)
            throw new ArgumentOutOfRangeException(nameof(dataLength), dataLength, $"dataLength > {MaxDataLength}");

        return (int)(((uint)dataLength + 2) / 3) * 4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetPaddingLength(int dataLength)
    {
        // Calculation is:
        // switch (dataLength % 3)
        // 0 -> 0
        // 1 -> 2
        // 2 -> 1
        int mod3 = FastMod3(dataLength);
        return mod3 == 0 ? 0 : (3 - mod3);
        //---------------------------------------------------------------------
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static int FastMod3(int value)
        {
            if (Environment.Is64BitProcess)
            {
                // We use modified Daniel Lemire's fastmod algorithm (https://github.com/dotnet/runtime/pull/406),
                // which allows to avoid the long multiplication if the divisor is less than 2**31.
                Debug.Assert(value <= int.MaxValue);

                ulong lowbits = (ulong.MaxValue / 3 + 1) * (uint)value;

#if NET5_0_OR_GREATER
                uint highbits = (uint)Math.BigMul(lowbits, 3, out _);
#else
                // 64bit * 64bit => 128bit isn't currently supported by Math https://github.com/dotnet/runtime/issues/31184
                // otherwise we'd want this to be (uint)Math.BigMul(lowbits, divisor, out _)
                uint highbits = (uint)((((lowbits >> 32) + 1) * 3) >> 32);
#endif

                Debug.Assert(highbits == value % 3);
                return (int)highbits;
            }
            else
            {
                return value % 3;
            }
        }
    }

    public static int GetEncodedLength(int dataLength)
        => GetMaxEncodedLength(dataLength) - GetPaddingLength(dataLength);

    public OperationStatus Encode(
        ReadOnlySpan<byte> data,
        Span<byte> encoded,
        out int consumed,
        out int written,
        bool isFinalBlock = true,
        byte pad = 0,
        int encodedLength = -1)
    {
        if (data.IsEmpty)
        {
            consumed = 0;
            written = 0;
            return OperationStatus.Done;
        }

        nuint srcLength = (uint)data.Length;
        nuint destLength = (uint)encoded.Length;
        ref byte srcBytes = ref MemoryMarshal.GetReference(data);
        ref byte dest = ref MemoryMarshal.GetReference(encoded);

        if (encodedLength < 0)
        {
            encodedLength = GetMaxEncodedLength((int)srcLength);
            if (pad == 0) encodedLength -= GetPaddingLength((int)srcLength);
        }

        nuint sourceIndex = 0;
        nuint destIndex = 0;
        nuint maxSrcLength;

        if (srcLength <= MaxDataLength && destLength >= (uint)encodedLength)
        {
            maxSrcLength = srcLength;
        }
        else
        {
            maxSrcLength = (destLength >> 2) * 3;
        }

        maxSrcLength -= 2;

        if ((nint)sourceIndex < (nint)maxSrcLength)
        {
            do
            {
                UnsafeBase64.Encode24(_bytes,
                    ref Unsafe.AddByteOffset(ref srcBytes, sourceIndex),
                    ref Unsafe.AddByteOffset(ref dest, destIndex));

                destIndex += 4;
                sourceIndex += 3;
            }
            while (sourceIndex < maxSrcLength);
        }

        if (maxSrcLength != srcLength - 2) goto DestinationTooSmallExit;

        if (!isFinalBlock)
        {
            if (sourceIndex == srcLength) goto DoneExit;

            goto NeedMoreDataExit;
        }

        if (sourceIndex == srcLength - 1)
        {
            UnsafeBase64.Encode8(_bytes,
                ref Unsafe.AddByteOffset(ref srcBytes, sourceIndex),
                ref Unsafe.AddByteOffset(ref dest, destIndex));

            destIndex += 2;

            if (pad > 0)
            {
                Unsafe.AddByteOffset(ref dest, destIndex++) = pad;
                Unsafe.AddByteOffset(ref dest, destIndex++) = pad;
            }

            sourceIndex += 1;
        }
        else if (sourceIndex == srcLength - 2)
        {
            UnsafeBase64.Encode16(_bytes,
                ref Unsafe.AddByteOffset(ref srcBytes, sourceIndex),
                ref Unsafe.AddByteOffset(ref dest, destIndex));

            destIndex += 3;

            if (pad > 0)
            {
                Unsafe.AddByteOffset(ref dest, destIndex++) = pad;
            }

            sourceIndex += 2;
        }

    DoneExit:
        consumed = (int)sourceIndex;
        written = (int)destIndex;
        return OperationStatus.Done;

    NeedMoreDataExit:
        consumed = (int)sourceIndex;
        written = (int)destIndex;
        return OperationStatus.NeedMoreData;

    DestinationTooSmallExit:
        consumed = (int)sourceIndex;
        written = (int)destIndex;
        return OperationStatus.DestinationTooSmall;
    }

    #region Encode128

    public EncodingStatus TryEncode128(Int128 value, Span<byte> encoded)
    {
        if (encoded.Length < 22) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode128(_bytes, ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode128(Int128 value, Span<char> encoded)
    {
        if (encoded.Length < 22) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode128(_chars, ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode128(Int128 value, Span<byte> encoded)
    {
        if (encoded.Length < 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 22");

        UnsafeBase64.Encode128(_bytes, ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode128(Int128 value, Span<char> encoded)
    {
        if (encoded.Length < 22) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 22");

        UnsafeBase64.Encode128(_chars, ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    public byte[] Encode128ToBytes(Int128 value)
    {
        var encoded = new byte[22];

        UnsafeBase64.Encode128(_bytes, ref Unsafe.As<Int128, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public char[] Encode128ToChars(Int128 value)
    {
        var encoded = new char[22];

        UnsafeBase64.Encode128(_chars, ref Unsafe.As<Int128, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public string Encode128ToString(Int128 value) => string.Create(22, value, (chars, value) =>
    {
        UnsafeBase64.Encode128(_chars, ref Unsafe.As<Int128, byte>(ref value), ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode128

    #region Encode72

    public EncodingStatus TryEncode72(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 9) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 12) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode72(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode72(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 9) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 12) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode72(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode72<T>(T value, Span<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 12) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode72(_bytes, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode72<T>(T value, Span<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 12) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode72(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode72(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 9) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 9");
        if (encoded.Length < 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 12");

        UnsafeBase64.Encode72(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode72(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 9) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 9");
        if (encoded.Length < 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 12");

        UnsafeBase64.Encode72(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode72<T>(T value, Span<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length < 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 12");

        UnsafeBase64.Encode72(_bytes, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode72<T>(T value, Span<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length < 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 12");

        UnsafeBase64.Encode72(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte[] Encode72ToBytes<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");

        var encoded = new byte[12];

        UnsafeBase64.Encode72(_bytes, ref Unsafe.As<T, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public char[] Encode72ToChars<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        var encoded = new char[12];

        UnsafeBase64.Encode72(_chars, ref Unsafe.As<T, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public string Encode72ToString<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");

        return string.Create(12, value, (chars, value) =>
        {
            UnsafeBase64.Encode72(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(chars));
        });
    }

    #endregion Encode72

    #region Encode64

    public EncodingStatus TryEncode64(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 8) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 11) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode64(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode64(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 8) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 11) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode64(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode64(ulong value, Span<byte> encoded)
    {
        if (encoded.Length < 11) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode64(_bytes, ref Unsafe.As<ulong, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode64(ulong value, Span<char> encoded)
    {
        if (encoded.Length < 11) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode64(_chars, ref Unsafe.As<ulong, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode64(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 8) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 8");
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        UnsafeBase64.Encode64(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode64(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 8) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 8");
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        UnsafeBase64.Encode64(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode64(ulong value, Span<byte> encoded)
    {
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        UnsafeBase64.Encode64(_bytes, ref Unsafe.As<ulong, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode64(ulong value, Span<char> encoded)
    {
        if (encoded.Length < 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 11");

        UnsafeBase64.Encode64(_chars, ref Unsafe.As<ulong, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    public byte[] Encode64ToBytes(ulong value)
    {
        var encoded = new byte[11];

        UnsafeBase64.Encode64(_bytes, ref Unsafe.As<ulong, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public char[] Encode64ToChars(ulong value)
    {
        var encoded = new char[11];

        UnsafeBase64.Encode64(_chars, ref Unsafe.As<ulong, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public string Encode64ToString(ulong value) => string.Create(11, value, (chars, value) =>
    {
        UnsafeBase64.Encode64(_chars, ref Unsafe.As<ulong, byte>(ref value), ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode64

    #region Encode32

    public EncodingStatus TryEncode32(uint value, Span<byte> encoded)
    {
        if (encoded.Length < 6) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode32(_bytes, ref Unsafe.As<uint, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode32(uint value, Span<char> encoded)
    {
        if (encoded.Length < 6) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode32(_chars, ref Unsafe.As<uint, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode32(uint value, Span<byte> encoded)
    {
        if (encoded.Length < 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 6");

        UnsafeBase64.Encode32(_bytes, ref Unsafe.As<uint, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode32(uint value, Span<char> encoded)
    {
        if (encoded.Length < 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 6");

        UnsafeBase64.Encode32(_chars, ref Unsafe.As<uint, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    public byte[] Encode32ToBytes(uint value)
    {
        var encoded = new byte[6];

        UnsafeBase64.Encode32(_bytes, ref Unsafe.As<uint, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public char[] Encode32ToChars(uint value)
    {
        var encoded = new char[6];

        UnsafeBase64.Encode32(_chars, ref Unsafe.As<uint, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public string Encode32ToString(uint value) => string.Create(6, value, (chars, value) =>
    {
        UnsafeBase64.Encode32(_chars, ref Unsafe.As<uint, byte>(ref value), ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode32

    #region Encode24

    public EncodingStatus TryEncode24(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 3) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 4) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode24(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode24(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 3) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 4) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode24(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode24<T>(T value, Span<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 4) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode24(_bytes, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode24<T>(T value, Span<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) return EncodingStatus.InvalidDataLength;
        if (encoded.Length < 4) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode24(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode24(ReadOnlySpan<byte> bytes, Span<byte> encoded)
    {
        if (bytes.Length != 3) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 3");
        if (encoded.Length < 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 4");

        UnsafeBase64.Encode24(_bytes, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode24(ReadOnlySpan<byte> bytes, Span<char> encoded)
    {
        if (bytes.Length != 3) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, "length != 3");
        if (encoded.Length < 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 4");

        UnsafeBase64.Encode24(_chars, ref MemoryMarshal.GetReference(bytes), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode24<T>(T value, Span<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length < 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 4");

        UnsafeBase64.Encode24(_bytes, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode24<T>(T value, Span<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length < 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 4");

        UnsafeBase64.Encode24(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Encode24ToInt32<T>(T value)
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");

        return UnsafeBase64.Encode24ToInt32(_bytes, ref Unsafe.As<T, byte>(ref value));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte[] Encode24ToBytes<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");

        var encoded = new byte[4];

        UnsafeBase64.Encode24(_bytes, ref Unsafe.As<T, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public char[] Encode24ToChars<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        var encoded = new char[4];

        UnsafeBase64.Encode24(_chars, ref Unsafe.As<T, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public string Encode24ToString<T>(T value) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");

        return string.Create(4, value, (chars, value) =>
        {
            UnsafeBase64.Encode24(_chars, ref Unsafe.As<T, byte>(ref value), ref MemoryMarshal.GetReference(chars));
        });
    }

    #endregion Encode24

    #region Encode16

    public EncodingStatus TryEncode16(ushort value, Span<byte> encoded)
    {
        if (encoded.Length < 3) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode16(_bytes, ref Unsafe.As<ushort, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode16(ushort value, Span<char> encoded)
    {
        if (encoded.Length < 3) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode16(_chars, ref Unsafe.As<ushort, byte>(ref value), ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode16(ushort value, Span<byte> encoded)
    {
        if (encoded.Length < 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 3");

        UnsafeBase64.Encode16(_bytes, ref Unsafe.As<ushort, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode16(ushort value, Span<char> encoded)
    {
        if (encoded.Length < 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 3");

        UnsafeBase64.Encode16(_chars, ref Unsafe.As<ushort, byte>(ref value), ref MemoryMarshal.GetReference(encoded));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode16(ushort value, out byte encoded0, out byte encoded1, out byte encoded2)
    {
        var map = _bytes;
        //TODO: refactoring
        ref byte src = ref Unsafe.As<ushort, byte>(ref value);
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8;
        encoded0 = map[i >> 18];
        encoded1 = map[i >> 12 & 0x3F];
        encoded2 = map[i >> 6 & 0x3F];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode16(ushort value, out char encoded0, out char encoded1, out char encoded2)
    {
        var map = _chars;
        ref byte src = ref Unsafe.As<ushort, byte>(ref value);
        int i = src << 16 | Unsafe.AddByteOffset(ref src, 1) << 8;
        encoded0 = map[i >> 18];
        encoded1 = map[i >> 12 & 0x3F];
        encoded2 = map[i >> 6 & 0x3F];
    }

    public byte[] Encode16ToBytes(ushort value)
    {
        var encoded = new byte[3];

        UnsafeBase64.Encode16(_bytes, ref Unsafe.As<ushort, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public char[] Encode16ToChars(ushort value)
    {
        var encoded = new char[3];

        UnsafeBase64.Encode16(_chars, ref Unsafe.As<ushort, byte>(ref value), ref encoded[0]);

        return encoded;
    }

    public string Encode16ToString(ushort value) => string.Create(3, value, (chars, value) =>
    {
        UnsafeBase64.Encode16(_chars, ref Unsafe.As<ushort, byte>(ref value), ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode16

    #region Encode8

    public EncodingStatus TryEncode8(byte value, Span<byte> encoded)
    {
        if (encoded.Length < 2) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode8(_bytes, ref value, ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    public EncodingStatus TryEncode8(byte value, Span<char> encoded)
    {
        if (encoded.Length < 2) return EncodingStatus.InvalidDestinationLength;

        UnsafeBase64.Encode8(_chars, ref value, ref MemoryMarshal.GetReference(encoded));

        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode8(byte value, Span<byte> encoded)
    {
        if (encoded.Length < 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 2");

        UnsafeBase64.Encode8(_bytes, ref value, ref MemoryMarshal.GetReference(encoded));
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Encode8(byte value, Span<char> encoded)
    {
        if (encoded.Length < 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length < 2");

        UnsafeBase64.Encode8(_chars, ref value, ref MemoryMarshal.GetReference(encoded));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode8(byte value, out byte encoded0, out byte encoded1)
    {
        var map = _bytes;
        int i = value << 8;
        encoded0 = map[i >> 10];
        encoded1 = map[i >> 4 & 0x3F];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode8(byte value, out char encoded0, out char encoded1)
    {
        var map = _chars;
        int i = value << 8;
        encoded0 = map[i >> 10];
        encoded1 = map[i >> 4 & 0x3F];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public short Encode8ToInt16(byte value) => UnsafeBase64.Encode8ToInt16(_bytes, ref value);

    public byte[] Encode8ToBytes(byte value)
    {
        var encoded = new byte[2];

        UnsafeBase64.Encode8(_bytes, ref value, ref encoded[0]);

        return encoded;
    }

    public char[] Encode8ToChars(byte value)
    {
        var encoded = new char[2];

        UnsafeBase64.Encode8(_chars, ref value, ref encoded[0]);

        return encoded;
    }

    public string Encode8ToString(byte value) => string.Create(2, value, (chars, value) =>
    {
        UnsafeBase64.Encode8(_chars, ref value, ref MemoryMarshal.GetReference(chars));
    });

    #endregion Encode8
}