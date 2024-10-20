using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

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

    [Test]
    public void Test128()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test128(new Int128((ulong)random.NextInt64(), (ulong)random.NextInt64()));
        }
    }

    private string Test128(Int128 value)
    {
        var str = VectorBase64Url.Encode128ToString(value);
        Assert.That(str, Is.EqualTo(new string(VectorBase64Url.Encode128ToChars(value))));

        const int len = 22;
        Int128 defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(VectorBase64Url.TryEncode128(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        VectorBase64Url.Encode128(value, bytes);
        Assert.That(bytes.SequenceEqual(VectorBase64Url.Encode128ToBytes(value)), Is.True);

        VectorBase64Url.Valid128(bytes);
        Assert.That(VectorBase64Url.TryValid128(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(VectorBase64Url.TryValid128(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(VectorBase64Url.TryEncode128(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        VectorBase64Url.Encode128(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        VectorBase64Url.Valid128(chars);
        Assert.That(VectorBase64Url.TryValid128(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(VectorBase64Url.TryValid128(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(VectorBase64Url.TryDecode128(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(VectorBase64Url.TryDecode128(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(VectorBase64Url.Decode128(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        decoded = default;
        Assert.That(VectorBase64Url.TryDecode128(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(VectorBase64Url.TryDecode128(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(VectorBase64Url.Decode128(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid128(bytes, chars);

        Assert.That(VectorBase64Url.TryDecode128(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(VectorBase64Url.TryDecode128(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        return str;
    }

    private void Invalid128(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        Int128 value = default;
        Int128 defaultValue = default;
        var m = Base64Decoder.Url.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(VectorBase64Url.TryValid128(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(VectorBase64Url.TryDecode128(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(VectorBase64Url.TryValid128(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(VectorBase64Url.TryDecode128(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(VectorBase64Url.TryValid128(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(VectorBase64Url.TryDecode128(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(VectorBase64Url.TryValid128(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(VectorBase64Url.TryDecode128(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(VectorBase64Url.TryValid128(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(VectorBase64Url.TryDecode128(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(VectorBase64Url.TryValid128(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(VectorBase64Url.TryDecode128(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }
}