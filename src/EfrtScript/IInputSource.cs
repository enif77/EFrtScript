/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


public interface IInputSource
{
    int CurrentChar { get; }

    int NextChar();
    string? ReadWordFromSource();
    string ReadStringFromSource();
    bool TryParseNumber(string? s, out IValue numericValue);
}
