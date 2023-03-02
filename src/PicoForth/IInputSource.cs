/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IInputSource
{
    int CurrentChar { get; }

    int NextChar();
    string? ReadWordFromSource();
    string ReadStringFromSource();
}
