/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


internal class DepthWord : IWord
{
    public string Name => "DEPTH";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.GetStackDepth()));

        return 1;
    }
}
