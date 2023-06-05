/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Tools.Words;


internal class ByeWord : IWord
{
    public string Name => "BYE";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.TerminateExecution();

        return 1;
    }
}

/*

https://forth-standard.org/standard/tools/BYE

BYE

Return control to the host operating system, if any.

*/