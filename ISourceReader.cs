/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface ISourceReader
{
    int CurrentChar { get; }

    int NextChar();
}
