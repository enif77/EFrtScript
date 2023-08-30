/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class LeaveWord : IWord
{
    public string Name => "LEAVE";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);

        interpreter.WordBeingDefined!.AddWord(
            new LeaveControlWord());

            return 1;     
    }
}

/*

https://forth-standard.org/standard/core/LEAVE

LEAVE

Discard the current loop control parameters. An ambiguous condition exists if they are unavailable.
Continue execution immediately following the innermost syntactically enclosing DO...LOOP or DO...+LOOP.

Rationale:

Note that LEAVE immediately exits the loop. No words following LEAVE within the loop will be executed. Typical use:

   : X ... DO ... IF ... LEAVE THEN ... LOOP ... ;

*/