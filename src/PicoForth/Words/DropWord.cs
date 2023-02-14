/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DropWord : IWord
{
    public string Name => "DROP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPop();

        return 1;
    }
}
