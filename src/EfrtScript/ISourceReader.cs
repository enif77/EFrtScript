/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


public interface ISourceReader
{
    int CurrentChar { get; }

    int NextChar();
}
