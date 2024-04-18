/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;


/// <summary>
/// An integer value.
/// </summary>
internal class IntegerValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer { get; }
    public double Float => Integer;
    public string String => Integer.ToString(CultureInfo.InvariantCulture);


    public IntegerValue(int value)
    {
        Integer = value;
    }
}
