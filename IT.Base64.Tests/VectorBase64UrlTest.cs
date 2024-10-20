using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Base64.Tests;

public class VectorBase64UrlTest
{
    [Test]
    public void Test128Url()
    {
        Span<byte> buffer = stackalloc byte[16];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), Int128.MaxValue);

        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("////////////////////fw"));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), Int128.MinValue);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("AAAAAAAAAAAAAAAAAAAAgA"));

        var value = new Int128(10468201550123809991, 12468201550123809992);
        var str = VectorBase64Url.Encode128ToString(value);
        Assert.That(str, Is.EqualTo("yLwaH0DzB63HvFLQ2IVGkQ"));
        Assert.That(VectorBase64Url.Decode128(str), Is.EqualTo(value));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), value);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo(str));

        Assert.That(VectorBase64Url.Encode128ToString(Int128.MaxValue), Is.EqualTo("____________________fw"));

        value = new Int128(10468201550123809991, 12468201550123822335);
        Assert.That(VectorBase64Url.Encode128ToString(value), Is.EqualTo("_-waH0DzB63HvFLQ2IVGkQ"));
        Assert.That(VectorBase64Url.Decode128("_-waH0DzB63HvFLQ2IVGkQ"), Is.EqualTo(value));
    }
}