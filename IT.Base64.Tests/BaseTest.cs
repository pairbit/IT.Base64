using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace IT.Base64.Tests;

public class BaseTest
{
    [Test]
    public void DecodingMap()
    {
        var decodingMap = Base64Encoder.Url.GetDecodingMap();

        Assert.That(decodingMap.AsSpan().SequenceEqual(Base64Decoder.Url.Map.Span), Is.True);

        decodingMap = Base64Encoder.IT.GetDecodingMap();

        Assert.That(decodingMap.AsSpan().SequenceEqual(Base64Decoder.IT.Map.Span), Is.True);
    }
    
    [Test]
    public void EncodeDecodeOther()
    {
        var data = "My secret string"u8;
        var encoded = new string('\0', 22);
        UnsafeBase64.Encode128(
            ref MemoryMarshal.GetReference(Base64Encoder.IT.Chars.Span),
            ref MemoryMarshal.GetReference(data),
            ref Unsafe.AsRef(in encoded.GetPinnableReference()));

        var decoded = new byte[16];
        UnsafeBase64.TryDecode128(
            ref MemoryMarshal.GetReference(Base64Decoder.IT.Map.Span),
            ref Unsafe.AsRef(in encoded.GetPinnableReference()),
            ref decoded[0]);

        Assert.That(data.SequenceEqual(decoded), Is.True);

        var base64 = Convert.FromBase64String(encoded + "==");

        Assert.That(data.SequenceEqual(base64), Is.False);
    }

    public static void ShowMap(sbyte[] map)
    {
        Console.WriteLine("{");
        for (int i = 0; i < map.Length; i++)
        {
            var code = map[i];
            if (code == -1)
                Console.WriteLine($"{code,2}, //{i}");
            else
                Console.WriteLine($"{code,2}, //{i} -> {(char)i}");
        }
        Console.WriteLine("};");
    }

    [Test]
    public void SizeOfTest()
    {
        Assert.That(Unsafe.SizeOf<Struct24>, Is.EqualTo(3));
        Assert.That(Unsafe.SizeOf<Struct72>, Is.EqualTo(9));
        Assert.That(Unsafe.SizeOf<Struct80>, Is.EqualTo(10));
        Assert.That(Unsafe.SizeOf<Struct176>, Is.EqualTo(22));

        Assert.That(Unsafe.SizeOf<Base64Decoder>, Is.EqualTo(8));
    }

    [Test]
    public void IntrinsicsTest()
    {
        Console.WriteLine($"OSArchitecture: {RuntimeInformation.OSArchitecture}");

        Assert.That(BitConverter.IsLittleEndian, Is.True);
        Assert.That(Vector128.IsHardwareAccelerated, Is.True);
        Assert.That(Ssse3.IsSupported || AdvSimd.Arm64.IsSupported, Is.True);

        if (Ssse3.IsSupported) Console.WriteLine("Ssse3");
        if (AdvSimd.Arm64.IsSupported) Console.WriteLine("Arm64");

        Assert.That(Vector128<short>.Count, Is.EqualTo(8));
        Assert.That(Vector128<byte>.Count, Is.EqualTo(16));
        Assert.That(Vector256<byte>.Count, Is.EqualTo(32));
    }
}