/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class ThrowWord : IWord
{
    public string Name => "THROW";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        // Wont return (throws an exception) when n is not zero and the CATCH word was executed.
        interpreter.Throw(interpreter.StackPop().Integer);

        return 1;
    }
}
