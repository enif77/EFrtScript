/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class DupWord : IWord
{
    public string Name => "DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.StackPeek());

        return 1;
    }
}
