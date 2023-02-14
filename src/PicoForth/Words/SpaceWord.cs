/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SpaceWord : IWord
{
    public string Name => "SPACE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write(" ");

        return 1;
    }
}
