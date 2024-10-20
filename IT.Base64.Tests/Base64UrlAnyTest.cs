namespace IT.Base64.Tests;

public class Base64UrlAnyTest : Base64Test
{
    public Base64UrlAnyTest() : base(Base64Encoder.Url, Base64Decoder.Any) { }

    [Test]
    public void Test8UrlAny()
    {
        Assert.That(_decoder.TryValid8("+/"), Is.EqualTo(DecodingStatus.Done));

        Assert.That(_decoder.TryValid8("-/", out var invalid), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalid, Is.EqualTo('\0'));
    }
}