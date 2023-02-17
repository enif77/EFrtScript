/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write($"{interpreter.StackPop().Integer}");

        return 1;
    }
}
