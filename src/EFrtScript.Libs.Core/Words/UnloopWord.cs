/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class UnloopWord : IWord
{
    public string Name => "UNLOOP";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        
        interpreter.WordBeingDefined!.AddWord(
            new UnloopControlWord());

            return 1;     
    }
}

/*

https://forth-standard.org/standard/core/UNLOOP

UNLOOP

Discard the loop-control parameters for the current nesting level. An UNLOOP is required for each nesting
level before the definition may be EXITed. An ambiguous condition exists if the loop-control parameters
are unavailable.

Typical use:

: X ...
   limit first DO
   ... test IF ... UNLOOP EXIT THEN ...
   LOOP ...
;

UNLOOP allows the use of EXIT within the context of DO ... LOOP and related do-loop constructs.

*/
