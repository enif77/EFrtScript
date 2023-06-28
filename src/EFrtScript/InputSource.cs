/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


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

    
    public int ReadChar()
    {
        return _source.NextChar();
    }


    public string? ReadWord()
    {
        return _parser.ParseWord();
    }


    public string ReadString()
    {
        return _parser.ParseString();
    }
}
