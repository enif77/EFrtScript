/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class EqualToZeroWord : IWord
{
    public string Name => "0=";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(interpreter.StackPop().Integer == 0 ? -1 : 0);

        return 1;
    }
}
