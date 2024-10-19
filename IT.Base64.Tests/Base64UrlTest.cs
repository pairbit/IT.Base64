using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Base64.Tests;

public class Base64UrlTest : Base64Test
{
    public Base64UrlTest() : base(Base64Encoder.Url, Base64Decoder.Url) { }

    [Test]
    public void Test8Url()
    {
        var by = byte.MaxValue;
        Assert.That(Convert.ToBase64String(new Span<byte>(ref by)).TrimEnd('='), Is.EqualTo("/w"));

        by = byte.MinValue;
        Assert.That(Convert.ToBase64String(new Span<byte>(ref by)).TrimEnd('='), Is.EqualTo("AA"));

        Assert.That(Test8(byte.MinValue), Is.EqualTo("AA"));
        Assert.That(Test8(byte.MaxValue), Is.EqualTo("_w"));
        Assert.That(Test8(236), Is.EqualTo("7A"));
        Assert.That(Test8(44), Is.EqualTo("LA"));

        Assert.That(_encoder.Encode8ToString(251), Is.EqualTo("-w"));
        Assert.That(_decoder.Decode8("-w"), Is.EqualTo(251));
        Assert.That(_decoder.Decode8("-_"), Is.EqualTo(251));

        Assert.That(_decoder.TryValid8("-/", out var invalid), Is.EqualTo(EncodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('/'));
        Assert.That(_decoder.TryValid8("+_", out invalid), Is.EqualTo(EncodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('+'));
        Assert.That(_decoder.TryValid8("+/"), Is.EqualTo(EncodingStatus.InvalidData));
    }

    [Test]
    public void Test16Url()
    {
        Span<byte> buffer = stackalloc byte[sizeof(ushort)];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), ushort.MaxValue);

        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("//8"));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), ushort.MinValue);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("AAA"));

        Assert.That(Test16(ushort.MinValue), Is.EqualTo("AAA"));
        Assert.That(Test16(ushort.MaxValue), Is.EqualTo("__8"));
        Assert.That(Test16(13623), Is.EqualTo("NzU"));
        Assert.That(Test16(44345), Is.EqualTo("Oa0"));
    }

    [Test]
    public void Test24Url()
    {
        Assert.That(Test24(new Struct24(ushort.MinValue, byte.MinValue)), Is.EqualTo("AAAA"));
        Assert.That(Test24(new Struct24(ushort.MaxValue, byte.MaxValue)), Is.EqualTo("____"));
        Assert.That(Test24(new Struct24(52353, 1)), Is.EqualTo("gcwB"));
        Assert.That(Test24(new Struct24(31349, 1)), Is.EqualTo("dXoB"));
        Assert.That(Test24(new Struct24(1041, 0)), Is.EqualTo("EQQA"));
    }

    [Test]
    public void Test32Url()
    {
        Span<byte> buffer = stackalloc byte[sizeof(uint)];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), uint.MaxValue);

        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("/////w"));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), uint.MinValue);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("AAAAAA"));

        Assert.That(Test32(uint.MinValue), Is.EqualTo("AAAAAA"));
        Assert.That(Test32(uint.MaxValue), Is.EqualTo("_____w"));
        Assert.That(Test32(1046820453), Is.EqualTo("ZTZlPg"));
        Assert.That(Test32(1481472373), Is.EqualTo("dXlNWA"));
    }

    [Test]
    public void Test64Url()
    {
        Assert.That(Test64(ulong.MinValue), Is.EqualTo("AAAAAAAAAAA"));
        Assert.That(Test64(ulong.MaxValue), Is.EqualTo("__________8"));
        Assert.That(Test64(1046820155012380993), Is.EqualTo("QXk7e2INhw4"));
        Assert.That(Test64(14814723746819689979), Is.EqualTo("-027hsF4mM0"));
    }

    [Test]
    public void Test72Url()
    {
        Assert.That(Test72(new Struct72(ulong.MinValue, byte.MinValue)), Is.EqualTo("AAAAAAAAAAAA"));
        Assert.That(Test72(new Struct72(ulong.MaxValue, byte.MaxValue)), Is.EqualTo("____________"));
        Assert.That(Test72(new Struct72(2235381257586707384, 1)), Is.EqualTo("uIsFBGmrBR8B"));
        Assert.That(Test72(new Struct72(13495389474433244262, 1)), Is.EqualTo("ZpwjsTlBSbsB"));
        Assert.That(Test72(new Struct72(10416025566214361379, 0)), Is.EqualTo("IxGY5RAojZAA"));
    }

    [Test]
    public void Test128Url()
    {
        Span<byte> buffer = stackalloc byte[16];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), Int128.MaxValue);

        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("////////////////////fw"));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), Int128.MinValue);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo("AAAAAAAAAAAAAAAAAAAAgA"));

        var value = new Int128(10468201550123809991, 12468201550123809992);
        var str = _encoder.Encode128ToString(value);
        Assert.That(str, Is.EqualTo("yLwaH0DzB63HvFLQ2IVGkQ"));
        Assert.That(_decoder.Decode128(str), Is.EqualTo(value));

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), value);
        Assert.That(Convert.ToBase64String(buffer).TrimEnd('='), Is.EqualTo(str));

        Assert.That(_encoder.Encode128ToString(Int128.MaxValue), Is.EqualTo("____________________fw"));

        value = new Int128(10468201550123809991, 12468201550123822335);
        Assert.That(_encoder.Encode128ToString(value), Is.EqualTo("_-waH0DzB63HvFLQ2IVGkQ"));
        Assert.That(_decoder.Decode128("_-waH0DzB63HvFLQ2IVGkQ"), Is.EqualTo(value));
    }
}