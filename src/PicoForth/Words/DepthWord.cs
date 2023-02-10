/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class DepthWord : IWord
{
    public string Name => "DEPTH";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(new IntValue(interpreter.StackDepth));
    }
}
