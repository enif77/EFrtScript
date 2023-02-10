/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class PlusWord : IWord
{
    public string Name => "+";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer + interpreter.StackPop().Integer));
    }
}
