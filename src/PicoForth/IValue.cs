/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IValue
{
    bool Boolean { get; }
    int Integer { get; }
    string String { get; }
}
