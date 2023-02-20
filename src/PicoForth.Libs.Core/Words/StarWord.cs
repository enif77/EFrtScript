/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class StarWord : IWord
{
    public string Name => "*";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.StackPop().Integer * interpreter.StackPop().Integer);

        return 1;
    }
}
