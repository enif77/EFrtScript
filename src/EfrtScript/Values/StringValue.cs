/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Globalization;


/// <summary>
/// A string value.
/// </summary>
internal class StringValue : IValue
{
    public bool Boolean => string.IsNullOrEmpty(String) == false;

    public int Integer
    {
        get
        {
            if (int.TryParse(String, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
            {
                return i;
            }

            throw new Exception($"'{String}' cannot be converted to an Integer");
        }
    }
    
    public double Real
    {
        get
        {
            if (double.TryParse(String, NumberStyles.Float, CultureInfo.InvariantCulture, out var i))
            {
                return i;
            }

            throw new Exception($"'{String}' cannot be converted to an Real");
        }
    }
    
    public string String { get; }


    public StringValue(string value)
    {
        String = value ?? string.Empty;
    }
}
