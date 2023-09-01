/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;


internal class TestInputSource : IInputSource
{
    private readonly ISourceReader _source;

    public int CurrentChar => _source.CurrentChar;
    

    public TestInputSource(ISourceReader source)
    {
        _source = source;
    }

    
    public int ReadChar()
    {
        return _source.NextChar();
    }


    public string? ReadWord()
    {
        return "test";
    }


    public string ReadString()
    {
        return "test-string";
    }
}
