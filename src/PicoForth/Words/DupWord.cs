/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DupWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.StackPeek());

        return 1;
    }
}
