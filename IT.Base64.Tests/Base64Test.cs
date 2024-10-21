using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace IT.Base64.Tests;

public abstract class Base64Test
{
    protected readonly Base64Encoder _encoder;
    protected readonly Base64Decoder _decoder;

    public Base64Test(Base64Encoder encoder, Base64Decoder decoder)
    {
        _encoder = encoder;
        _decoder = decoder;
    }

    [Test]
    public void Test8()
    {
        for (var i = 0; i <= byte.MaxValue; i++)
        {
            Test8((byte)i);
        }
    }

    [Test]
    public void Test16()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test16((ushort)random.Next());
        }
    }

    [Test]
    public void Test24()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test24(new Struct24((ushort)random.Next(), (byte)random.Next()));
        }
    }

    [Test]
    public void Test32()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test32((uint)random.Next());
        }
    }

    [Test]
    public void Test64()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test64((ulong)random.NextInt64());
        }
    }

    [Test]
    public void Test72()
    {
        var random = Random.Shared;
        for (var i = 0; i < 100; i++)
        {
            Test72(new Struct72((ulong)random.NextInt64(), (byte)random.Next()));
        }
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

    [Test]
    public void Test()
    {
        var random = Random.Shared;
        for (int i = 0; i < 100; i++)
        {
            var len = random.Next(128, 1000);

            var bytes = new byte[len];

            random.NextBytes(bytes);

            Test(bytes, hasPadding: true);
            Test(bytes, hasPadding: false);
        }
    }

    protected string Test8(byte value)
    {
        var str = _encoder.Encode8ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode8ToChars(value))));

        const int len = 2;
        byte defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode8(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode8(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode8ToBytes(value)), Is.True);

        _decoder.Valid8(bytes);
        Assert.That(_decoder.TryValid8(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid8(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode8(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode8(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid8(chars);
        Assert.That(_decoder.TryValid8(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid8(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode8(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode8(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.Decode8(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        decoded = default;
        Assert.That(_decoder.TryDecode8(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode8(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.Decode8(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid8(bytes, chars);

        Assert.That(_decoder.TryDecode8(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode8(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode8(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode8(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode8(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode8(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode8(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode8(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        _encoder.Encode8(value, out byte byte0, out byte byte1);
        Assert.That(bytes[0], Is.EqualTo(byte0));
        Assert.That(bytes[1], Is.EqualTo(byte1));

        _encoder.Encode8(value, out char char0, out char char1);
        Assert.That(chars[0], Is.EqualTo(char0));
        Assert.That(chars[1], Is.EqualTo(char1));

        Int16 int16 = default;
        UnsafeBase64.Encode8(ref MemoryMarshal.GetReference(_encoder.Bytes.Span), ref value, ref Unsafe.As<short, byte>(ref int16));
        Assert.That(_encoder.Encode8ToInt16(value), Is.EqualTo(int16));
        Assert.That(Base64Encoded.ToString(int16), Is.EqualTo(str));
        Assert.That(Base64Encoded.ToString((ushort)int16), Is.EqualTo(str));
        Assert.That(Base64Encoded.To<short>(str), Is.EqualTo(int16));
        Assert.That(Base64Encoded.TryTo<short>(str, out var uint16_2), Is.True);
        Assert.That(uint16_2, Is.EqualTo(int16));
        Assert.That(Base64Encoded.TryTo<int>(str, out var int32), Is.False);
        Assert.That(int32, Is.EqualTo(default(int)));
        return str;
    }

    protected void Invalid8(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        byte value = default;
        byte defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid8(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode8(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid8(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode8(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid8(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode8(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid8(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode8(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid8(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode8(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid8(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode8(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test16(ushort value)
    {
        var str = _encoder.Encode16ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode16ToChars(value))));

        const int len = 3;
        byte defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode16(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode16(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode16ToBytes(value)), Is.True);

        _decoder.Valid16(bytes);
        Assert.That(_decoder.TryValid16(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid16(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode16(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode16(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid16(chars);
        Assert.That(_decoder.TryValid16(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid16(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode16(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode16(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.Decode16(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        decoded = default;
        Assert.That(_decoder.TryDecode16(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode16(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.Decode16(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid16(bytes, chars);

        Assert.That(_decoder.TryDecode16(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode16(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode16(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode16(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode16(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode16(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode16(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode16(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        _encoder.Encode16(value, out byte byte0, out byte byte1, out byte byte2);
        Assert.That(bytes[0], Is.EqualTo(byte0));
        Assert.That(bytes[1], Is.EqualTo(byte1));
        Assert.That(bytes[2], Is.EqualTo(byte2));

        _encoder.Encode16(value, out char char0, out char char1, out char char2);
        Assert.That(chars[0], Is.EqualTo(char0));
        Assert.That(chars[1], Is.EqualTo(char1));
        Assert.That(chars[2], Is.EqualTo(char2));

        //ushort ushort1 = default;
        //UnsafeBase64.Encode16(Base64Url.Bytes, ref value, ref Unsafe.As<ushort, byte>(ref ushort1));
        //Base64Url.Encode16(value, out ushort ushort2);
        //Assert.That(ushort1, Is.EqualTo(ushort2));
        //Assert.That(Base64.ToString(ushort1), Is.EqualTo(str));
        //Assert.That(Base64.ToString((short)ushort1), Is.EqualTo(str));
        return str;
    }

    protected void Invalid16(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        ushort value = default;
        ushort defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid16(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode16(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid16(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode16(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid16(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode16(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid16(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode16(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid16(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode16(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid16(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode16(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test24<T>(T value) where T : unmanaged
    {
        var str = _encoder.Encode24ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode24ToChars(value))));

        const int len = 4;
        T defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode24(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode24(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode24ToBytes(value)), Is.True);

        _decoder.Valid24(bytes);
        Assert.That(_decoder.TryValid24(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid24(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode24(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode24(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid24(chars);
        Assert.That(_decoder.TryValid24(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid24(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Span<byte> buffer = stackalloc byte[Unsafe.SizeOf<T>()];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), value);

        bytes.Clear();
        Assert.That(_encoder.TryEncode24(buffer, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(_decoder.TryDecode24<T>(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode24(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));
        bytes.Clear();
        _encoder.Encode24(buffer, bytes);
        Assert.That(_decoder.Decode24<T>(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        chars.Clear();
        Assert.That(_encoder.TryEncode24(buffer, chars), Is.EqualTo(EncodingStatus.Done));
        decoded = default;
        Assert.That(_decoder.TryDecode24(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode24(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));
        chars.Clear();
        _encoder.Encode24(buffer, chars);
        Assert.That(_decoder.Decode24<T>(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid24(bytes, chars);

        Assert.That(_decoder.TryDecode24(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode24(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode24(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode24(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode24<ulong>(bytes, out var longDest), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(chars, out longDest), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode24(bytes, out longDest, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode24(chars, out longDest, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Int32 int32 = default;
        UnsafeBase64.Encode24(ref MemoryMarshal.GetReference(_encoder.Bytes.Span), ref Unsafe.As<T, byte>(ref value), ref Unsafe.As<int, byte>(ref int32));
        Assert.That(_encoder.Encode24ToInt32(value), Is.EqualTo(int32));
        Assert.That(Base64Encoded.ToString(int32), Is.EqualTo(str));
        Assert.That(Base64Encoded.ToString((uint)int32), Is.EqualTo(str));
        Assert.That(Base64Encoded.To<int>(str), Is.EqualTo(int32));
        Assert.That(Base64Encoded.TryTo<int>(str, out var int32_2), Is.True);
        Assert.That(int32_2, Is.EqualTo(int32));
        Assert.That(Base64Encoded.TryTo<short>(str, out var int16), Is.False);
        Assert.That(int16, Is.EqualTo(default(short)));

        return str;
    }

    protected void Invalid24(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        Struct24 value = default;
        Struct24 defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid24(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode24(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid24(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode24(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid24(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode24(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid24(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode24(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid24(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode24(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid24(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode24(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test32(uint value)
    {
        var str = _encoder.Encode32ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode32ToChars(value))));

        const int len = 6;
        uint defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode32(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode32(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode32ToBytes(value)), Is.True);

        _decoder.Valid32(bytes);
        Assert.That(_decoder.TryValid32(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid32(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode32(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode32(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid32(chars);
        Assert.That(_decoder.TryValid32(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid32(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode32(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode32(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.Decode32(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        decoded = default;
        Assert.That(_decoder.TryDecode32(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode32(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.Decode32(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid32(bytes, chars);

        Assert.That(_decoder.TryDecode32(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode32(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode32(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode32(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode32(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode32(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode32(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode32(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        return str;
    }

    protected void Invalid32(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        uint value = default;
        uint defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid32(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode32(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid32(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode32(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid32(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode32(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid32(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode32(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid32(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode32(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid32(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode32(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test64(ulong value)
    {
        var str = _encoder.Encode64ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode64ToChars(value))));

        const int len = 11;
        ulong defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode64(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode64(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode64ToBytes(value)), Is.True);

        _decoder.Valid64(bytes);
        Assert.That(_decoder.TryValid64(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid64(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode64(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode64(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid64(chars);
        Assert.That(_decoder.TryValid64(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid64(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Span<byte> buffer = stackalloc byte[sizeof(ulong)];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), value);

        bytes.Clear();
        Assert.That(_encoder.TryEncode64(buffer, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(_decoder.TryDecode64(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode64(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));
        bytes.Clear();
        _encoder.Encode64(buffer, bytes);
        Assert.That(_decoder.Decode64(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        chars.Clear();
        Assert.That(_encoder.TryEncode64(buffer, chars), Is.EqualTo(EncodingStatus.Done));
        decoded = default;
        Assert.That(_decoder.TryDecode64(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode64(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));
        chars.Clear();
        _encoder.Encode64(buffer, chars);
        Assert.That(_decoder.Decode64(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid64(bytes, chars);

        Assert.That(_decoder.TryDecode64(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode64(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode64(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode64(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode64(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode64(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode64(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode64(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        return str;
    }

    protected void Invalid64(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        ulong value = default;
        ulong defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid64(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode64(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid64(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode64(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid64(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode64(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid64(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode64(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid64(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode64(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid64(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode64(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test72<T>(T value) where T : unmanaged
    {
        var str = _encoder.Encode72ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode72ToChars(value))));

        const int len = 12;
        T defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode72(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode72(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode72ToBytes(value)), Is.True);

        _decoder.Valid72(bytes);
        Assert.That(_decoder.TryValid72(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid72(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode72(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode72(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid72(chars);
        Assert.That(_decoder.TryValid72(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid72(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Span<byte> buffer = stackalloc byte[Unsafe.SizeOf<T>()];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(buffer), value);

        bytes.Clear();
        Assert.That(_encoder.TryEncode72(buffer, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(_decoder.TryDecode72<T>(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode72(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));
        bytes.Clear();
        _encoder.Encode72(buffer, bytes);
        Assert.That(_decoder.Decode72<T>(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        chars.Clear();
        Assert.That(_encoder.TryEncode72(buffer, chars), Is.EqualTo(EncodingStatus.Done));
        decoded = default;
        Assert.That(_decoder.TryDecode72(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode72(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));
        chars.Clear();
        _encoder.Encode72(buffer, chars);
        Assert.That(_decoder.Decode72<T>(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid72(bytes, chars);

        Assert.That(_decoder.TryDecode72(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode72(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode72(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode72(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode72<ulong>(bytes, out var longDest), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(chars, out longDest), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode72(bytes, out longDest, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode72(chars, out longDest, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDestinationLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        return str;
    }

    protected void Invalid72(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        Struct72 value = default;
        Struct72 defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid72(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode72(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid72(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode72(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid72(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode72(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid72(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode72(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid72(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode72(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid72(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode72(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected string Test128(Int128 value)
    {
        var str = _encoder.Encode128ToString(value);
        Assert.That(str, Is.EqualTo(new string(_encoder.Encode128ToChars(value))));

        const int len = 22;
        Int128 defaultValue = default;
        Span<byte> bytes = stackalloc byte[len];
        Assert.That(_encoder.TryEncode128(value, bytes), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));
        bytes.Clear();
        _encoder.Encode128(value, bytes);
        Assert.That(bytes.SequenceEqual(_encoder.Encode128ToBytes(value)), Is.True);

        _decoder.Valid128(bytes);
        Assert.That(_decoder.TryValid128(bytes), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid128(bytes, out var invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Span<char> chars = stackalloc char[len];
        Assert.That(_encoder.TryEncode128(value, chars), Is.EqualTo(EncodingStatus.Done));
        Assert.That(str, Is.EqualTo(new string(chars)));
        chars.Clear();
        _encoder.Encode128(value, chars);
        Assert.That(str, Is.EqualTo(new string(chars)));

        _decoder.Valid128(chars);
        Assert.That(_decoder.TryValid128(chars), Is.EqualTo(DecodingStatus.Done));
        Assert.That(_decoder.TryValid128(chars, out var invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode128(bytes, out var decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode128(bytes, out decoded, out invalidByte), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.Decode128(bytes), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(Encoding.ASCII.GetString(bytes)));

        decoded = default;
        Assert.That(_decoder.TryDecode128(chars, out decoded), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        decoded = default;
        Assert.That(_decoder.TryDecode128(chars, out decoded, out invalidChar), Is.EqualTo(DecodingStatus.Done));
        Assert.That(decoded, Is.EqualTo(value));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.Decode128(chars), Is.EqualTo(value));
        Assert.That(str, Is.EqualTo(new string(chars)));

        Invalid128(bytes, chars);

        Assert.That(_decoder.TryDecode128(stackalloc byte[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode128(stackalloc byte[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode128(stackalloc byte[len - 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode128(stackalloc byte[len + 1], out decoded, out invalidByte), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidByte, Is.EqualTo(default(byte)));

        Assert.That(_decoder.TryDecode128(stackalloc char[len - 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode128(stackalloc char[len + 1], out decoded), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));

        Assert.That(_decoder.TryDecode128(stackalloc char[len - 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        Assert.That(_decoder.TryDecode128(stackalloc char[len + 1], out decoded, out invalidChar), Is.EqualTo(DecodingStatus.InvalidDataLength));
        Assert.That(decoded, Is.EqualTo(defaultValue));
        Assert.That(invalidChar, Is.EqualTo(default(char)));

        return str;
    }

    protected void Invalid128(ReadOnlySpan<byte> bytes, ReadOnlySpan<char> chars)
    {
        Assert.That(bytes.Length, Is.EqualTo(chars.Length));

        Int128 value = default;
        Int128 defaultValue = default;
        var m = _decoder.Map.Span;
        var offset = bytes.Length - 1;
        Span<byte> invalidBytes = stackalloc byte[bytes.Length];
        Span<char> invalidChars = stackalloc char[bytes.Length];
        for (byte b = 0; b < 255; b++)
        {
            if (m[b] != -1) continue;

            bytes.CopyTo(invalidBytes);
            invalidBytes[offset] = b;
            Assert.That(_decoder.TryValid128(invalidBytes), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode128(invalidBytes, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid128(invalidBytes, out var invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidByte, Is.EqualTo(b));
            invalidByte = default;
            Assert.That(_decoder.TryDecode128(invalidBytes, out value, out invalidByte), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidByte, Is.EqualTo(b));

            chars.CopyTo(invalidChars);
            invalidChars[offset] = (char)b;
            Assert.That(_decoder.TryValid128(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode128(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid128(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)b));
            invalidChar = default;
            Assert.That(_decoder.TryDecode128(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)b));

            if (--offset < 0) offset = bytes.Length - 1;
        }
        offset = 256;
        for (int i = 0; i < invalidChars.Length; i++)
        {
            chars.CopyTo(invalidChars);
            invalidChars[i] = (char)offset;
            Assert.That(_decoder.TryValid128(invalidChars), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(_decoder.TryDecode128(invalidChars, out value), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));

            Assert.That(_decoder.TryValid128(invalidChars, out var invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            invalidChar = default;
            Assert.That(_decoder.TryDecode128(invalidChars, out value, out invalidChar), Is.EqualTo(DecodingStatus.InvalidData));
            Assert.That(value, Is.EqualTo(defaultValue));
            Assert.That(invalidChar, Is.EqualTo((char)offset));
            offset++;
        }
    }

    protected virtual ReadOnlySpan<byte> Test(ReadOnlySpan<byte> bytes, bool hasPadding)
    {
        var maxEncodedLength = Base64Encoder.GetMaxEncodedLength(bytes.Length);
        var paddingLength = Base64Encoder.GetPaddingLength(bytes.Length);

        int encodedLength = maxEncodedLength - paddingLength;

        var encoded = new byte[hasPadding ? maxEncodedLength : encodedLength];

        var status = _encoder.Encode(bytes, encoded, out var consumed, out var written);

        if (hasPadding)
        {
            for (int i = 0; i < paddingLength; i++)
            {
                encoded[i + encodedLength] = (byte)'=';
            }
        }

        Assert.That(status, Is.EqualTo(OperationStatus.Done));
        Assert.That(consumed, Is.EqualTo(bytes.Length));
        Assert.That(written, Is.EqualTo(encodedLength));

        return encoded;
    }
}