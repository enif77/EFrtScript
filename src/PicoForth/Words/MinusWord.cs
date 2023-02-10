/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class MinusWord : IWord
{
    public string Name => "-";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        var b = interpreter.StackPop().Integer;
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer - b));
    }
}
