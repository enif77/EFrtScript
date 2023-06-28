/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;


/// <summary>
/// A string value.
/// </summary>
internal class StringValue : IValue
{
    public bool Boolean => string.IsNullOrEmpty(String) == false;

    public int Integer => throw new NotSupportedException($"'{String}' cannot be converted to an integer number.");

    public double Float => throw new NotSupportedException($"'{String}' cannot be converted to a floating point number.");

    public string String { get; }


    public StringValue(string value)
    {
        String = value;
    }
}
