using IT.Base64.Internal;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace IT.Base64;

public static class VectorBase64Url
{
    private static readonly char[] _chars = Base64Encoder.Url._chars;
    private static readonly byte[] _bytes = Base64Encoder.Url._bytes;
    private static readonly sbyte[] _map = Base64Decoder.Url._map;

    #region Public

    public static void Encode96(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
        }
        else
        {
            //TODO: ref MemoryMarshal.GetArrayDataReference(_bytes) ???
            UnsafeBase64.Encode96(ref _bytes[0], ref src, ref encoded);
        }
    }

    public static void Encode96(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
        }
        else
        {
            UnsafeBase64.Encode96(ref _chars[0], ref src, ref encoded);
        }
    }

    public static void Encode104(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode8(ref _bytes[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            UnsafeBase64.Encode104(ref _bytes[0], ref src, ref encoded);
        }
    }

    public static void Encode104(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode8(ref _chars[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            UnsafeBase64.Encode104(ref _chars[0], ref src, ref encoded);
        }
    }

    public static void Encode112(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode16(ref _bytes[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            UnsafeBase64.Encode112(ref _bytes[0], ref src, ref encoded);
        }
    }

    public static void Encode112(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode16(ref _chars[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            UnsafeBase64.Encode112(ref _chars[0], ref src, ref encoded);
        }
    }

    public static void Encode120(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode24(ref _bytes[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            UnsafeBase64.Encode120(ref _bytes[0], ref src, ref encoded);
        }
    }

    public static void Encode120(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            UnsafeBase64.Encode24(ref _chars[0], ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            UnsafeBase64.Encode120(ref _chars[0], ref src, ref encoded);
        }
    }

    public static void Encode128(ref byte src, ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            ref var bytes = ref _bytes[0];
            UnsafeBase64.Encode24(ref bytes, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 16));
            UnsafeBase64.Encode8(ref bytes, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 20));
        }
        else
        {
            UnsafeBase64.Encode128(ref _bytes[0], ref src, ref encoded);
        }
    }

    public static void Encode128(ref byte src, ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            Vector128Base64Url.Encode96(ref src, ref encoded);
            ref var chars = ref _chars[0];
            UnsafeBase64.Encode24(ref chars, ref Unsafe.AddByteOffset(ref src, 12), ref Unsafe.AddByteOffset(ref encoded, 32));
            UnsafeBase64.Encode8(ref chars, ref Unsafe.AddByteOffset(ref src, 15), ref Unsafe.AddByteOffset(ref encoded, 40));
        }
        else
        {
            UnsafeBase64.Encode128(ref _chars[0], ref src, ref encoded);
        }
    }

    public static bool TryDecode96(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src);
        }
        else
        {
            return UnsafeBase64.TryDecode96(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode96(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src);
        }
        else
        {
            return UnsafeBase64.TryDecode96(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode104(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode104(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode104(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode104(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode112(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode112(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode112(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode112(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode120(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode120(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode120(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode120(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode128(ref byte encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode128(ref char encoded, ref byte src)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.TryDecode96(ref encoded, ref src) &&
                UnsafeBase64.TryDecode32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12));
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref _map[0], ref encoded, ref src);
        }
    }

    public static bool TryDecode96(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode96(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode96(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode96(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode104(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode104(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode104(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode104(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode112(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode112(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode112(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode112(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode120(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode120(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode120(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode120(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode128(ref byte encoded, ref byte src, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool TryDecode128(ref char encoded, ref byte src, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return VectorDecode96(ref encoded, ref src, out invalid) &&
                UnsafeBase64.TryDecode32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), ref Unsafe.AddByteOffset(ref src, 12), out invalid);
        }
        else
        {
            return UnsafeBase64.TryDecode128(ref _map[0], ref encoded, ref src, out invalid);
        }
    }

    public static bool IsValid96(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded);
        }
        else
        {
            return UnsafeBase64.IsValid96(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid96(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded);
        }
        else
        {
            return UnsafeBase64.IsValid96(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid104(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            return UnsafeBase64.IsValid104(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid104(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            return UnsafeBase64.IsValid104(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid112(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            return UnsafeBase64.IsValid112(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid112(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            return UnsafeBase64.IsValid112(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid120(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            return UnsafeBase64.IsValid120(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid120(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            return UnsafeBase64.IsValid120(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid128(ref byte encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16));
        }
        else
        {
            return UnsafeBase64.IsValid128(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid128(ref char encoded)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return Vector128Base64Url.IsValid96(ref encoded) &&
                UnsafeBase64.IsValid32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32));
        }
        else
        {
            return UnsafeBase64.IsValid128(ref _map[0], ref encoded);
        }
    }

    public static bool IsValid96(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid96(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid96(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid96(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid104(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid104(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid104(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid8(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid104(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid112(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid112(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid112(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid16(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid112(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid120(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid120(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid120(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid24(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid120(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid128(ref byte encoded, out byte invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 16), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid128(ref _map[0], ref encoded, out invalid);
        }
    }

    public static bool IsValid128(ref char encoded, out char invalid)
    {
        if (BitConverter.IsLittleEndian && Vector128.IsHardwareAccelerated && (Ssse3.IsSupported || AdvSimd.Arm64.IsSupported))
        {
            return IsVectorValid96(ref encoded, out invalid) &&
                UnsafeBase64.IsValid32(ref _map[0], ref Unsafe.AddByteOffset(ref encoded, 32), out invalid);
        }
        else
        {
            return UnsafeBase64.IsValid128(ref _map[0], ref encoded, out invalid);
        }
    }

    #endregion Public

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsVectorValid96(ref byte encoded, out byte invalid)
    {
        if (!Vector128Base64Url.IsValid96(ref encoded))
        {
            invalid = UnsafeBase64.GetInvalid(ref _map[0], ref encoded, 16);
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsVectorValid96(ref char encoded, out char invalid)
    {
        if (!Vector128Base64Url.IsValid96(ref encoded))
        {
            invalid = UnsafeBase64.GetInvalid(ref _map[0], ref encoded, 32);
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool VectorDecode96(ref byte encoded, ref byte src, out byte invalid)
    {
        if (!Vector128Base64Url.TryDecode96(ref encoded, ref src))
        {
            invalid = UnsafeBase64.GetInvalid(ref _map[0], ref encoded, 16);
            return false;
        }
        invalid = default;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool VectorDecode96(ref char encoded, ref byte src, out char invalid)
    {
        if (!Vector128Base64Url.TryDecode96(ref encoded, ref src))
        {
            invalid = UnsafeBase64.GetInvalid(ref _map[0], ref encoded, 32);
            return false;
        }
        invalid = default;
        return true;
    }
}