namespace IT.Base64.Tests;

public class Base64DefaultTest : Base64Test
{
    public Base64DefaultTest() : base(Base64Encoder.Default, Base64Decoder.Default) { }
}