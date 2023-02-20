/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;


internal class CrWord : IWord
{
    public string Name => "CR";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.WriteLine();

        return 1;
    }
}
