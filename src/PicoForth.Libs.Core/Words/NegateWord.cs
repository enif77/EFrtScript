/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class NegateWord : IWord
{
    public string Name => "NEGATE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.StackPush(-interpreter.StackPop().Integer);

        return 1;
    }
}
