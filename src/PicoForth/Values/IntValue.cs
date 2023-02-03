/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Values;

using System.Globalization;


internal class IntValue : IValue
{
    public bool Boolean => Int != 0;
    public int Int { get; }
    public string String => Int.ToString(CultureInfo.InvariantCulture);


    public IntValue(int value)
    {
        Int = value;
    }
}
