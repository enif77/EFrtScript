/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class AbortWord : IWord
{
    public string Name => "ABORT";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Throw(-1);

        return 1;
    }
}
