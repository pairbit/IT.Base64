using System.Linq;
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
        Assert.That(Unsafe.SizeOf<Struct176>, Is.EqualTo(22));
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