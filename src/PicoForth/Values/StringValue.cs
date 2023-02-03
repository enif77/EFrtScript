/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Values;

using System.Globalization;


internal class StringValue : IValue
{
    public bool Boolean => string.IsNullOrEmpty(String) == false;

    public int Int
    {
        get
        {
            if (int.TryParse(String, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
            {
                return i;
            }

            throw new Exception($"'{String}' cannot be converted to an Int");
        }
    }
    
    public string String { get; }


    public StringValue(string value)
    {
        String = value ?? string.Empty;
    }
}
