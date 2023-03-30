/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Text;

using EFrtScript.Values;


internal class InputSource : IInputSource
{
    private readonly ISourceReader _source;
    private readonly Parser _parser;

    public int CurrentChar => _source.CurrentChar;
    

    public InputSource(ISourceReader source)
    {
        _source = source;
        _parser = new Parser(source);
    }

    
    public int NextChar()
    {
        return _source.NextChar();
    }


    public string? ReadWordFromSource()
    {
        return _parser.NextWord();
    }


    public string ReadStringFromSource()
    {
        var sb = new StringBuilder();
        var c = NextChar();  // Skip the white-space behind the string literal opening word (S., .", ...).
        while (c >= 0)
        {
            if (c == '"')
            {
                break;
            }

            sb.Append((char)c);

            c = NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A string literal end expected");
        }

        return sb.ToString();
    }


    public bool TryParseNumber(string? s, out IValue numericValue)
    {
        if (int.TryParse(s, out var val))
        {
            numericValue = new IntegerValue(val);

            return true;
        }

        numericValue = new IntegerValue(0);

        return false;
    }
}
