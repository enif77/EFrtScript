/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class SlashWord : IWord
{
    public string Name => "/";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        var b = interpreter.StackPop().Integer;
        interpreter.StackPush(interpreter.StackPop().Integer / b);

        return 1;
    }
}
