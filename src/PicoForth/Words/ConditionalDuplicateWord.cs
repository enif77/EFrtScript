/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


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
            interpreter.StackPush(new IntValue(0));
        }

        return 1;
    }
}
