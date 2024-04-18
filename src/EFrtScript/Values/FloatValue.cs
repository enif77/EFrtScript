/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;


/// <summary>
/// A real (floating point) value.
/// </summary>
internal class FloatValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer => (int) Float;
    public double Float { get; }
    public string String => Float.ToString(CultureInfo.InvariantCulture);


    public FloatValue(double value)
    {
        Float = value;
    }
}
