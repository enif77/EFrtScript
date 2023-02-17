/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;

using PicoForth.Extensions;


internal class LessThanZeroWord : IWord
{
    public string Name => "0<";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.StackPop().Integer < 0 ? -1 : 0));

        return 1;
    }
}
