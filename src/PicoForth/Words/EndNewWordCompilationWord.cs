/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class EndNewWordCompilationWord : IWord
{
    public string Name => ";";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.EndNewWordCompilation();

        return 1;
    }
}
