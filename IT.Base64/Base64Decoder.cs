using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Base64;

public class Base64Decoder
{
    private readonly sbyte[] _map;

    public ReadOnlyMemory<sbyte> Map => _map;

    public Base64Decoder(ReadOnlySpan<sbyte> map)
    {
        _map = map.ToArray();
    }

    #region Decode72

    public EncodingStatus TryDecode72<T>(ReadOnlySpan<byte> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12) return EncodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 9) return EncodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode72<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12) return EncodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 9) return EncodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode72<T>(ReadOnlySpan<byte> encoded, out T value, out byte invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 9)
        {
            invalid = default;
            return EncodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode72<T>(ReadOnlySpan<char> encoded, out T value, out char invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 12)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 9)
        {
            invalid = default;
            return EncodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode72<T>(ReadOnlySpan<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");

        T value = default;
        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode72<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 9) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 9");
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");

        T value = default;
        if (!UnsafeBase64.TryDecode72(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");

        return value;
    }

    #endregion Decode72

    #region Valid72

    public EncodingStatus TryValid72(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 12) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid72(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 12) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid72(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 12)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid72(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 12)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid72(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");
        if (!UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid72(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 12) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 12");
        if (!UnsafeBase64.IsValid72(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");
    }

    #endregion Valid72

    #region Decode64

    public EncodingStatus TryDecode64(ReadOnlySpan<byte> encoded, out ulong value)
    {
        value = default;
        if (encoded.Length != 11) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode64(ReadOnlySpan<char> encoded, out ulong value)
    {
        value = default;
        if (encoded.Length != 11) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode64(ReadOnlySpan<byte> encoded, out ulong value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 11)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode64(ReadOnlySpan<char> encoded, out ulong value, out char invalid)
    {
        value = default;
        if (encoded.Length != 11)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ulong Decode64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");

        ulong value = 0;
        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ulong Decode64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");

        ulong value = 0;
        if (!UnsafeBase64.TryDecode64(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ulong, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode64

    #region Valid64

    public EncodingStatus TryValid64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid64(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 11)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid64(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 11)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid64(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");
        if (!UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid64(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 11) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 11");
        if (!UnsafeBase64.IsValid64(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid64

    #region Decode32

    public EncodingStatus TryDecode32(ReadOnlySpan<byte> encoded, out uint value)
    {
        value = default;
        if (encoded.Length != 6) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode32(ReadOnlySpan<char> encoded, out uint value)
    {
        value = default;
        if (encoded.Length != 6) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode32(ReadOnlySpan<byte> encoded, out uint value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 6)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode32(ReadOnlySpan<char> encoded, out uint value, out char invalid)
    {
        value = default;
        if (encoded.Length != 6)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public uint Decode32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");

        uint value = 0;
        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public uint Decode32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");

        uint value = 0;
        if (!UnsafeBase64.TryDecode32(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<uint, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode32

    #region Valid32

    public EncodingStatus TryValid32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid32(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 6)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid32(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 6)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid32(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");
        if (!UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid32(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 6) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 6");
        if (!UnsafeBase64.IsValid32(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid32

    #region Decode24

    public EncodingStatus TryDecode24<T>(ReadOnlySpan<byte> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4) return EncodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 3) return EncodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode24<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4) return EncodingStatus.InvalidDataLength;
        if (Unsafe.SizeOf<T>() != 3) return EncodingStatus.InvalidDestinationLength;

        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode24<T>(ReadOnlySpan<byte> encoded, out T value, out byte invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode24<T>(ReadOnlySpan<char> encoded, out T value, out char invalid) where T : unmanaged
    {
        value = default;
        if (encoded.Length != 4)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (Unsafe.SizeOf<T>() != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDestinationLength;
        }
        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode24<T>(ReadOnlySpan<byte> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");

        T value = default;
        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public T Decode24<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != 3) throw new ArgumentOutOfRangeException(nameof(T), Unsafe.SizeOf<T>(), $"SizeOf<{typeof(T).FullName}> != 3");
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");

        T value = default;
        if (!UnsafeBase64.TryDecode24(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<T, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");

        return value;
    }

    #endregion Decode24

    #region Valid24

    public EncodingStatus TryValid24(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 4) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid24(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 4) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid24(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 4)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid24(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 4)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid24(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");
        if (!UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid24(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 4) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 4");
        if (!UnsafeBase64.IsValid24(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "Invalid char");
    }

    #endregion Valid24

    #region Decode16

    public EncodingStatus TryDecode16(ReadOnlySpan<byte> encoded, out ushort value)
    {
        value = default;
        if (encoded.Length != 3) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode16(ReadOnlySpan<char> encoded, out ushort value)
    {
        value = default;
        if (encoded.Length != 3) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value)))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode16(ReadOnlySpan<byte> encoded, out ushort value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode16(ReadOnlySpan<char> encoded, out ushort value, out char invalid)
    {
        value = default;
        if (encoded.Length != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ushort Decode16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");

        ushort value = 0;
        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public ushort Decode16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");

        ushort value = 0;
        if (!UnsafeBase64.TryDecode16(_map, ref MemoryMarshal.GetReference(encoded), ref Unsafe.As<ushort, byte>(ref value), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode16

    #region Valid16

    public EncodingStatus TryValid16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid16(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid16(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 3)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid16(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");
        if (!UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid16(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 3) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 3");
        if (!UnsafeBase64.IsValid16(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid16

    #region Decode8

    public EncodingStatus TryDecode8(ReadOnlySpan<byte> encoded, out byte value)
    {
        value = default;
        if (encoded.Length != 2) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode8(ReadOnlySpan<char> encoded, out byte value)
    {
        value = default;
        if (encoded.Length != 2) return EncodingStatus.InvalidDataLength;

        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode8(ReadOnlySpan<byte> encoded, out byte value, out byte invalid)
    {
        value = default;
        if (encoded.Length != 2)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value, out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    public EncodingStatus TryDecode8(ReadOnlySpan<char> encoded, out byte value, out char invalid)
    {
        value = default;
        if (encoded.Length != 2)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value, out invalid))
        {
            value = default;
            return EncodingStatus.InvalidData;
        }
        return EncodingStatus.Done;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte Decode8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");

        byte value = 0;
        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value, out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");

        return value;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public byte Decode8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");

        byte value = 0;
        if (!UnsafeBase64.TryDecode8(_map, ref MemoryMarshal.GetReference(encoded), ref value, out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");

        return value;
    }

    #endregion Decode8

    #region Valid8

    public EncodingStatus TryValid8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) return EncodingStatus.InvalidDataLength;
        return UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded)) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid8(ReadOnlySpan<byte> encoded, out byte invalid)
    {
        if (encoded.Length != 2)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    public EncodingStatus TryValid8(ReadOnlySpan<char> encoded, out char invalid)
    {
        if (encoded.Length != 2)
        {
            invalid = default;
            return EncodingStatus.InvalidDataLength;
        }
        return UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded), out invalid) ? EncodingStatus.Done : EncodingStatus.InvalidData;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid8(ReadOnlySpan<byte> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");
        if (!UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid byte");
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public void Valid8(ReadOnlySpan<char> encoded)
    {
        if (encoded.Length != 2) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, "length != 2");
        if (!UnsafeBase64.IsValid8(_map, ref MemoryMarshal.GetReference(encoded), out var invalid))
            throw new ArgumentOutOfRangeException(nameof(encoded), invalid, "invalid char");
    }

    #endregion Valid8
}