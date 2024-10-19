using System;
using System.Runtime.CompilerServices;

namespace IT.Base64;

public class Base64Encoded
{
    public static string ToString(ushort encoded) => string.Create(2, encoded, static (chars, encoded) =>
    {
        ref byte b = ref Unsafe.As<ushort, byte>(ref encoded);
        chars[0] = (char)b;
        chars[1] = (char)Unsafe.AddByteOffset(ref b, 1);
    });

    public static string ToString<T>(T encoded) where T : unmanaged => string.Create(Unsafe.SizeOf<T>(), encoded, static (chars, encoded) =>
    {
        ref byte b = ref Unsafe.As<T, byte>(ref encoded);
        chars[0] = (char)b;
        for (int i = 1; i < chars.Length; i++)
        {
            chars[i] = (char)Unsafe.AddByteOffset(ref b, i);
        }
    });

    public static EncodingStatus TryToChars<T>(T encoded, Span<char> chars) where T : unmanaged
    {
        if (chars.Length < Unsafe.SizeOf<T>()) return EncodingStatus.InvalidDestinationLength;

        ref byte b = ref Unsafe.As<T, byte>(ref encoded);
        chars[0] = (char)b;
        for (int i = 1; i < chars.Length; i++)
        {
            chars[i] = (char)Unsafe.AddByteOffset(ref b, i);
        }
        return EncodingStatus.Done;
    }

    public static char[] ToChars<T>(T encoded) where T : unmanaged
    {
        var chars = new char[Unsafe.SizeOf<T>()];
        ref byte b = ref Unsafe.As<T, byte>(ref encoded);
        chars[0] = (char)b;
        for (int i = 1; i < chars.Length; i++)
        {
            chars[i] = (char)Unsafe.AddByteOffset(ref b, i);
        }
        return chars;
    }

    public static bool TryTo<T>(ReadOnlySpan<char> encoded, out T value) where T : unmanaged
    {
        value = default;
        if (Unsafe.SizeOf<T>() != encoded.Length) return false;

        ref byte b = ref Unsafe.As<T, byte>(ref value);

        b = (byte)encoded[0];
        for (int i = 1; i < encoded.Length; i++)
        {
            Unsafe.AddByteOffset(ref b, i) = (byte)encoded[i];
        }

        return true;
    }

    /// <exception cref="ArgumentOutOfRangeException"/>
    public static T To<T>(ReadOnlySpan<char> encoded) where T : unmanaged
    {
        if (Unsafe.SizeOf<T>() != encoded.Length) throw new ArgumentOutOfRangeException(nameof(encoded), encoded.Length, $"length != {Unsafe.SizeOf<T>()}");

        T value = default;
        ref byte b = ref Unsafe.As<T, byte>(ref value);

        b = (byte)encoded[0];
        for (int i = 1; i < encoded.Length; i++)
        {
            Unsafe.AddByteOffset(ref b, i) = (byte)encoded[i];
        }

        return value;
    }

    internal static void UnsafeToChar176(ref byte by, ref char ch)
    {
        ch = (char)by;
        Unsafe.AddByteOffset(ref ch, 2) = (char)Unsafe.AddByteOffset(ref by, 1);
        Unsafe.AddByteOffset(ref ch, 4) = (char)Unsafe.AddByteOffset(ref by, 2);
        Unsafe.AddByteOffset(ref ch, 6) = (char)Unsafe.AddByteOffset(ref by, 3);
        Unsafe.AddByteOffset(ref ch, 8) = (char)Unsafe.AddByteOffset(ref by, 4);
        Unsafe.AddByteOffset(ref ch, 10) = (char)Unsafe.AddByteOffset(ref by, 5);
        Unsafe.AddByteOffset(ref ch, 12) = (char)Unsafe.AddByteOffset(ref by, 6);
        Unsafe.AddByteOffset(ref ch, 14) = (char)Unsafe.AddByteOffset(ref by, 7);
        Unsafe.AddByteOffset(ref ch, 16) = (char)Unsafe.AddByteOffset(ref by, 8);
        Unsafe.AddByteOffset(ref ch, 18) = (char)Unsafe.AddByteOffset(ref by, 9);
        Unsafe.AddByteOffset(ref ch, 20) = (char)Unsafe.AddByteOffset(ref by, 10);
        Unsafe.AddByteOffset(ref ch, 22) = (char)Unsafe.AddByteOffset(ref by, 11);
        Unsafe.AddByteOffset(ref ch, 24) = (char)Unsafe.AddByteOffset(ref by, 12);
        Unsafe.AddByteOffset(ref ch, 26) = (char)Unsafe.AddByteOffset(ref by, 13);
        Unsafe.AddByteOffset(ref ch, 28) = (char)Unsafe.AddByteOffset(ref by, 14);
        Unsafe.AddByteOffset(ref ch, 30) = (char)Unsafe.AddByteOffset(ref by, 15);
        Unsafe.AddByteOffset(ref ch, 32) = (char)Unsafe.AddByteOffset(ref by, 16);
        Unsafe.AddByteOffset(ref ch, 34) = (char)Unsafe.AddByteOffset(ref by, 17);
        Unsafe.AddByteOffset(ref ch, 36) = (char)Unsafe.AddByteOffset(ref by, 18);
        Unsafe.AddByteOffset(ref ch, 38) = (char)Unsafe.AddByteOffset(ref by, 19);
        Unsafe.AddByteOffset(ref ch, 40) = (char)Unsafe.AddByteOffset(ref by, 20);
        Unsafe.AddByteOffset(ref ch, 42) = (char)Unsafe.AddByteOffset(ref by, 21);
    }
}