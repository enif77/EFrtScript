/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


internal class FetchReturnStackWord : IWord
{
    public string Name => "R@";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.ReturnStackPeek());
    }
}
