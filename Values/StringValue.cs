using System;
using System.Globalization;


internal class StringValue : IValue
{
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

    public long Long
    {
        get
        {
            if (long.TryParse(String, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
            {
                return i;
            }

            throw new Exception($"'{String}' cannot be converted to a Long");
        }
    }

    public string String { get; }


    public StringValue(string value)
    {
        String = value ?? string.Empty;
    }
}
