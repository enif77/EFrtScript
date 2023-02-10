/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class LessThanZeroWord : IWord
{
    public string Name => "0<";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer < 0 ? -1 : 0));
    }
}
