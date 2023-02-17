/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;


internal class PrintStringWord : IWord
{
    public string Name => "S.";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.Write(interpreter.StackPop().String);

        return 1;
    }
}
