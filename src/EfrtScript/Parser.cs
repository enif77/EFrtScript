/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Text;

using EFrtScript.Values;


internal class Parser
{
    private readonly ISourceReader _source;
    

    public Parser(ISourceReader source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }


    public string? NextWord()
    {
        StringBuilder? wordBuff = null;

        var c = _source.NextChar();
        while (c >= 0)
        {
            if (char.IsWhiteSpace((char)c))
            {
                if (wordBuff == null)
                {
                    c = _source.NextChar();

                    continue;
                }

                break;
            }

            wordBuff ??= new StringBuilder();
            wordBuff.Append((char) c);

            c = _source.NextChar();
        }

        return wordBuff?.ToString();
    }


    public string ReadString()
    {
        var stringBuff = new StringBuilder();
        var c = _source.NextChar();  // Skip the white-space behind the string literal opening word (S., .", ...).
        while (c >= 0)
        {
            if (c == '"')
            {
                break;
            }

            stringBuff.Append((char)c);

            c = _source.NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A string literal end expected");
        }

        return stringBuff.ToString();
    }


    /// <summary>
    /// Converts the string representation of a number to its signed integer or floating point equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">A string containing a number to convert.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <returns>tReturns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(string? s, out IValue result)
    {
        /*
         
            result – When this method returns, contains the 32-bit signed integer value equivalent of the number
            contained in s, if the conversion succeeded, or zero if the conversion failed. The conversion fails
            if the s parameter is null or Empty, is not of the correct format, or represents a number less than
            Int32.MinValue or greater than Int32.MaxValue. This parameter is passed uninitialized; any value
            originally supplied in result will be overwritten.
          
         */
        
        if (int.TryParse(s, out var val))
        {
            result = new IntegerValue(val);

            return true;
        }

        result = new IntegerValue(0);

        return false;
    }
}
