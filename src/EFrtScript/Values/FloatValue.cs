/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;
using System.Text.Json.Serialization;


/// <summary>
/// A real (floating point) value.
/// </summary>
internal class FloatValue : IValue
{
    [JsonIgnore]
    public bool Boolean => Integer != 0;

    [JsonIgnore]
    public int Integer => (int) Float;
    
    public double Float { get; }

    [JsonIgnore]
    public string String => Float.ToString(CultureInfo.InvariantCulture);


    public FloatValue(double value)
    {
        Float = value;
    }
}
