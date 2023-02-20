/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.Write(interpreter.StackPop().String);

        return 1;
    }
}
