/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.Output.Write(interpreter.StackPop().String);

        return 1;
    }
}
