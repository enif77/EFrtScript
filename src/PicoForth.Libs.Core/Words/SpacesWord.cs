/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class SpacesWord : IWord
{
    public string Name => "SPACES";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        var count = interpreter.StackPop().Integer;
        for (var i = 0; i < count; i++)
        {
            interpreter.Output.Write(" ");
        }

        return 1;
    }
}
