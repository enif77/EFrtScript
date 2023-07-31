/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class FetchReturnStackWord : IWord
{
    public string Name => "R@";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.ReturnStackPeek());

        return 1;
    }
}
