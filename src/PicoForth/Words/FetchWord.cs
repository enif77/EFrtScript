/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class FetchWord : IWord
{
    public string Name => "@";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.HeapFetch(interpreter.StackPop().Integer));

        return 1;
    }
}
