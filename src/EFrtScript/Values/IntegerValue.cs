/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;
using System.Text.Json.Serialization;


/// <summary>
/// An integer value.
/// </summary>
internal class IntegerValue : IValue
{
    [JsonIgnore]
    public bool Boolean => Integer != 0;
    
    public int Integer { get; }
    
    [JsonIgnore]
    public double Float => Integer;

    [JsonIgnore]
    public string String => Integer.ToString(CultureInfo.InvariantCulture);


    public IntegerValue(int value)
    {
        Integer = value;
    }
}
