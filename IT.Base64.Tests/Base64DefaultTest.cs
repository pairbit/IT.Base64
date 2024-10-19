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

        Assert.That(_decoder.TryValid8("+_", out var invalid), Is.EqualTo(EncodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('_'));

        Assert.That(_decoder.TryValid8("-/", out invalid), Is.EqualTo(EncodingStatus.InvalidData));
        Assert.That(invalid, Is.EqualTo('-'));

        Assert.That(_decoder.TryValid8("-/"), Is.EqualTo(EncodingStatus.InvalidData));
    }
}