/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DupWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.StackPeek());
    }
}
