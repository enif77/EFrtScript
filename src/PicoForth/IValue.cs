/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IValue
{
    bool Boolean { get; }
    int Int { get; }
    string String { get; }
}
