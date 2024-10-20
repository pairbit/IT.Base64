using System.Buffers;

namespace IT.Base64.Tests;

public class Base64DefaultTest : Base64Test
{
    public Base64DefaultTest() : base(Base64Encoder.Default, Base64Decoder.Default) { }

    [Test]
    public void Test8Default()
    {
        var by = byte.MaxValue;
        Assert.That(Convert.ToBase64String(new Span<byte>(ref by)).TrimEnd('='), Is.EqualTo("/w"));

        Assert.That(Test8(byte.MaxValue), Is.EqualTo("/w"));

        Assert.That(_encoder.Encode8ToString(251), Is.EqualTo("+w"));
        Assert.That(_decoder.Decode8("+w"), Is.EqualTo(251));
        Assert.That(_decoder.Decode8("+/"), Is.EqualTo(251));

        Assert.That(_decoder.TryValid8("+_", out var invalid), Is.EqualTo(DecodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('_'));

        Assert.That(_decoder.TryValid8("-/", out invalid), Is.EqualTo(DecodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('-'));

        Assert.That(_decoder.TryValid8("-/"), Is.EqualTo(DecodingStatus.InvalidData));
    }

    protected override ReadOnlySpan<byte> Test(ReadOnlySpan<byte> bytes, bool hasPadding)
    {
        var encoded = base.Test(bytes, hasPadding);

        var encodedLength = System.Buffers.Text.Base64.GetMaxEncodedToUtf8Length(bytes.Length);

        Span<byte> utf8 = stackalloc byte[encodedLength];

        var status = System.Buffers.Text.Base64.EncodeToUtf8(bytes, utf8, out var consumed, out var written);

        Assert.That(status, Is.EqualTo(OperationStatus.Done));
        Assert.That(consumed, Is.EqualTo(bytes.Length));
        Assert.That(written, Is.EqualTo(encodedLength));

        if (hasPadding)
        {
            Assert.That(encoded.Length, Is.EqualTo(encodedLength));
            Assert.That(encoded.SequenceEqual(utf8), Is.True);
        }
        else
        {
            var paddingLength = Base64Encoder.GetPaddingLength(bytes.Length);

            Assert.That(encoded.Length + paddingLength, Is.EqualTo(encodedLength));
            Assert.That(encoded.SequenceEqual(utf8[..encoded.Length]), Is.True);
        }

        return encoded;
    }
}