/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


internal class PlusWord : IWord
{
    public string Name => "+";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer + interpreter.StackPop().Integer));

        return 1;
    }
}
