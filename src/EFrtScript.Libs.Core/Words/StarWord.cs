/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class StarWord : IWord
{
    public string Name => "*";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        interpreter.StackPush(interpreter.StackPop().Integer * interpreter.StackPop().Integer);

        return 1;
    }
}
