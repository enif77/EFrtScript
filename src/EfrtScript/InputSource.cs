/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Text;


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
        return _parser.ReadString();
    }
}
