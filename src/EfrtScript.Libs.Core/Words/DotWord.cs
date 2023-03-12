/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class DotWord : IWord
{
    public string Name => ".";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.Output.Write($"{interpreter.StackPop().Integer}");

        return 1;
    }
}
