/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class LessThanWord : IWord
{
    public string Name => "<";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var b = interpreter.StackPop();
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer < b.Integer ? -1 : 0));

        return 1;
    }
}
