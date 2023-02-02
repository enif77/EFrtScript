/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


internal class StringSourceReader : ISourceReader
{
    public int CurrentChar =>
        (_srcPos < 0 || _srcPos >= _src.Length)
            ? -1
            : _src[_srcPos];


    public StringSourceReader(string src)
    {
        _srcPos = -1;
        _src = src ?? string.Empty;
    }


    public int NextChar()
    {
        var srcPos = _srcPos + 1;
        if (srcPos >= _src.Length)
        {
            return -1;
        }
         
        return _src[_srcPos = srcPos];
    }


    private int _srcPos = -1;
    private string _src = string.Empty;
}