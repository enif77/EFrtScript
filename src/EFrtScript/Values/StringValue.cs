/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Values;

using System.Text.Json.Serialization;

/// <summary>
/// A string value.
/// </summary>
internal class StringValue : IValue
{
    [JsonIgnore]
    public bool Boolean => string.IsNullOrEmpty(String) == false;

    [JsonIgnore]
    public int Integer => throw new NotSupportedException($"'{String}' cannot be converted to an integer number.");

    [JsonIgnore]
    public double Float => throw new NotSupportedException($"'{String}' cannot be converted to a floating point number.");

    public string String { get; }


    public StringValue(string value)
    {
        String = value;
    }
}
