/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.Write($"{interpreter.StackPop().Integer}");

        return 1;
    }
}
