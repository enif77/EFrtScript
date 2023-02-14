/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class FetchReturnStackWord : IWord
{
    public string Name => "R@";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.ReturnStackPeek());

        return 1;
    }
}
