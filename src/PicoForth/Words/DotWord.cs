/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write($"{interpreter.StackPop().Integer}");
    }
}
