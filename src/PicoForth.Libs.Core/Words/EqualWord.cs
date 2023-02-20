/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class EqualWord : IWord
{
    public string Name => "=";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        var b = interpreter.StackPop();
        interpreter.StackPush(interpreter.StackPop().Integer == b.Integer ? -1 : 0);

        return 1;
    }
}
