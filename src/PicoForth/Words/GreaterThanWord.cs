/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class GreaterThanWord : IWord
{
    public string Name => ">";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var b = interpreter.StackPop();
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer > b.Integer ? -1 : 0));

        return 1;
    }
}
