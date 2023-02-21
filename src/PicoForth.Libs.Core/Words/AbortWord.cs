/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;



internal class AbortWord : IWord
{
    public string Name => "ABORT";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Throw(-1);

        return 1;
    }
}

/*

ABORT

Empty the data stack and perform the function of QUIT, which includes emptying the return stack, without displaying a message.

https://forth-standard.org/standard/core/ABORT

*/