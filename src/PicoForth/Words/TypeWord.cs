/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write(interpreter.StackPop().String);

        return 1;
    }
}
