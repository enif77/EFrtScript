/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Words;

using EFrtScript.Extensions;


internal class CatchWord : IWord
{
    public string Name => "CATCH";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        interpreter.WordBeingDefined!
            .AddWord(new CatchControlWord(
                interpreter.WordBeingDefined,
                interpreter.WordBeingDefined.NextWordIndex));

        return 1;
    }
}

/*

https://forth-standard.org/standard/exception/CATCH

CATCH

( i * x xt -- j * x 0 | i * x n )
Push an exception frame on the exception stack and then execute the execution token xt (as with EXECUTE)
in such a way that control can be transferred to a point just after CATCH if THROW is executed during
the execution of xt.

If the execution of xt completes normally (i.e., the exception frame pushed by this CATCH is not popped
by an execution of THROW) pop the exception frame and return zero on top of the data stack, above whatever
stack items would have been returned by xt EXECUTE. Otherwise, the remainder of the execution semantics
are given by THROW.
 
 */