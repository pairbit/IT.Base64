using System.Runtime.InteropServices;

namespace IT.Base64.Tests;

[StructLayout(LayoutKind.Sequential, Size = 10)]
public readonly struct Struct80
{
    public ulong L0 { get; init; }

    public ushort S1 { get; init; }

    public Struct80(ulong l0, ushort s1)
    {
        L0 = l0;
        S1 = s1;
    }
}