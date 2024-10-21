using System.Buffers;

namespace IT.Base64.Tests;

public class Base64ITTest : Base64Test
{
    public Base64ITTest() : base(Base64Encoder.IT, Base64Decoder.IT) { }

    [Test]
    public void Test8Default()
    {
        Assert.That(_encoder.Encode8ToString(255), Is.EqualTo("-M"));
        Assert.That(_encoder.Encode8ToString(251), Is.EqualTo("_M"));
        Assert.That(_decoder.Decode8("-w"), Is.EqualTo(254));
        Assert.That(_decoder.Decode8("-_"), Is.EqualTo(255));
    }
}