/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;


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
            if (Parser.TryParseNumber(String, out var result))
            {
                return result.Integer;
            }

            // TODO: Throw a forth exception? (-24 invalid numeric argument)
            throw new Exception($"'{String}' cannot be converted to an integer number.");
        }
    }
    
    public double Float
    {
        get
        {
            if (Parser.TryParseNumber(String, out var result))
            {
                return result.Float;
            }

            // TODO: Throw a forth exception?
            throw new Exception($"'{String}' cannot be converted to a floating point number.");
        }
    }
    
    public string String { get; }


    public StringValue(string value)
    {
        String = value;
    }
}
