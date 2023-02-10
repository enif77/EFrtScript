/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class CrWord : IWord
{
    public string Name => "CR";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.WriteLine();
    }
}
