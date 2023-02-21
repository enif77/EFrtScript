/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class FetchReturnStackWord : IWord
{
    public string Name => "R@";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(1);
        interpreter.StackFree(1);

        interpreter.StackPush(interpreter.ReturnStackPeek());

        return 1;
    }
}
