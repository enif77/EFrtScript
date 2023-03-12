/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


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
