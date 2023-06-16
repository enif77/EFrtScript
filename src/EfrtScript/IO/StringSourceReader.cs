/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.IO;


/// <summary>
/// Reads script from a string.
/// </summary>
public class StringSourceReader : ISourceReader
{
    /// <inheritdoc cref="IInterpreter"/>
    public int CurrentChar =>
        (_srcPos < 0 || _srcPos >= _src.Length)
            ? -1
            : _src[_srcPos];
    
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="src">A string containing a script source.</param>
    public StringSourceReader(string src)
    {
        _srcPos = -1;
        _src = src ?? string.Empty;
    }


    /// <inheritdoc cref="IInterpreter"/>
    public int NextChar()
    {
        var srcPos = _srcPos + 1;
        if (srcPos < _src.Length)
        {
            return _src[_srcPos = srcPos];
        }
        
        _srcPos = _src.Length;
            
        return -1;
    }


    private int _srcPos;
    private readonly string _src;
}
