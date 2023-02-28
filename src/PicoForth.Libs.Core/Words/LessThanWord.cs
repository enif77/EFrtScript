/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class LessThanWord : IWord
{
    public string Name => "<";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop();
        interpreter.StackPush(interpreter.StackPop().Integer < b.Integer ? -1 : 0);

        return 1;
    }
}