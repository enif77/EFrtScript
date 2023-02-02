/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IValue
{
    int Int { get; }
    long Long { get; }
    string String { get; }
}
