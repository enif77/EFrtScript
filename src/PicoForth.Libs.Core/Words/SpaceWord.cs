/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;


internal class SpaceWord : IWord
{
    public string Name => "SPACE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.Write(" ");

        return 1;
    }
}