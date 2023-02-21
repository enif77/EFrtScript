/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class ConditionalDuplicateWord : IWord
{
    public string Name => "?DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var v = interpreter.StackPeek();
        if (v.Boolean)
        {
            interpreter.StackFree(1);
            interpreter.StackPush(v);
        }
        else
        {
            interpreter.StackPop();
            interpreter.StackPush(0);
        }

        return 1;
    }
}
