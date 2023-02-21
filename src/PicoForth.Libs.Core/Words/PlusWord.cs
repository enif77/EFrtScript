/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class PlusWord : IWord
{
    public string Name => "+";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);

        interpreter.StackPush(interpreter.StackPop().Integer + interpreter.StackPop().Integer);

        return 1;
    }
}
