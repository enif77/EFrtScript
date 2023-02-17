/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


internal class SlashWord : IWord
{
    public string Name => "/";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var b = interpreter.StackPop().Integer;
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer / b));

        return 1;
    }
}
