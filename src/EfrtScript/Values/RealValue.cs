/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;


/// <summary>
/// A real (floating point) value.
/// </summary>
internal class RealValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer => (int) Real;
    public double Real { get; }
    public string String => Real.ToString(CultureInfo.InvariantCulture);


    public RealValue(double value)
    {
        Real = value;
    }
}
