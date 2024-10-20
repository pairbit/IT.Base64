namespace IT.Base64.Tests;

public class Base64DefaultAnyTest : Base64Test
{
    public Base64DefaultAnyTest() : base(Base64Encoder.Default, Base64Decoder.Any) { }

    [Test]
    public void Test8DefaultAny()
    {
        Assert.That(_decoder.TryValid8("-/"), Is.EqualTo(DecodingStatus.Done));

        Assert.That(_decoder.TryValid8("+_", out var invalid), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalid, Is.EqualTo('\0'));
    }
}