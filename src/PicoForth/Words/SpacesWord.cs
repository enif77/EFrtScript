/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;


internal class SpacesWord : IWord
{
    public string Name => "SPACES";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var count = interpreter.StackPop().Integer;
        for (var i = 0; i < count; i++)
        {
            interpreter.OutputWriter.Write(" ");
        }

        return 1;
    }
}
