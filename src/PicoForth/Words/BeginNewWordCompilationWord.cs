/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class BeginNewWordCompilationWord : IWord
{
    public string Name => ":";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.BeginNewWordCompilation(
            interpreter.ReadWordFromSource() ?? throw new Exception("A new word name expected."));
    }
}
