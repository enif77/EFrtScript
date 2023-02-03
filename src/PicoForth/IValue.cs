/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IValue
{
    bool Boolean { get; }
    int Int { get; }
    long Long { get; }
    string String { get; }
}
