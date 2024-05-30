/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Words;
using EFrtScript.Extensions;


internal class LiteralWord : IWord
{
    public string Name => "LITERAL";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.StackExpect(1);

        interpreter.WordBeingDefined!
            .AddWord(new ConstantValueWord(
                interpreter.StackPop()));

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/LITERAL

LITERAL

immediate

Compilation:
   ( x -- )
   Append the run-time semantics given below to the current definition.
   
Run-time:
   ( -- x )
   Place x on the stack.
 
 */