/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Values;

using System.Globalization;


internal class IntValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer { get; }
    public string String => Integer.ToString(CultureInfo.InvariantCulture);


    public IntValue(int value)
    {
        Integer = value;
    }
}
