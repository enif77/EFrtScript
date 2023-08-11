/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class ExecuteWord : IWord
{
    public string Name => "EXECUTE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        return interpreter.ExecuteWord(
            interpreter.GetRegisteredWord(
            interpreter.StackPopInteger()));
    }
}

/*

https://forth-standard.org/standard/core/EXECUTE

EXECUTE

( xt -- )
Remove xt from the stack and perform the semantics identified by it. Other stack effects are due to the word EXECUTEd.
 
 */
 