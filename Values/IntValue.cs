/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Values;


internal class IntValue : IValue
{
    public int Int { get; }
    public long Long => (long)Int;
    public string String => $"{Int}";


    public IntValue(int value)
    {
        Int = value;
    }
}
