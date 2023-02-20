/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class ConditionalDuplicateWord : IWord
{
    public string Name => "?DUP";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var v = interpreter.StackPeek();
        if (v.Boolean)
        {
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
