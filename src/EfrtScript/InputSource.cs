/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Text;


internal class InputSource : IInputSource
{
    private readonly ISourceReader _source;
    private readonly Tokenizer _tokenizer;

    public int CurrentChar => _source.CurrentChar;
    

    public InputSource(ISourceReader source)
    {
        _source = source;
        _tokenizer = new Tokenizer(source);
    }

    
    public int NextChar()
    {
        return _source.NextChar();
    }


    public string? ReadWordFromSource()
    {
        return _tokenizer.NextWord();
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
}
