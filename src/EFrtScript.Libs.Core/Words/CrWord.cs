/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


internal class CrWord : IWord
{
    public string Name => "CR";
    public bool IsImmediate => false;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.Output.WriteLine();

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/CR

CR

( -- )
Cause subsequent output to appear at the beginning of the next line.
 
 */