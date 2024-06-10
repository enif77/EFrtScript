/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Words;

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class ConstantWord : IWord
{
    public string Name => "CONSTANT";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);
        
        interpreter.BeginNewWordCompilation(
            interpreter.CurrentInputSource!.ReadWord() ?? throw new Exception("A new word name expected."));
        interpreter.WordBeingDefined!
            .AddWord(new ConstantValueWord(
                interpreter.StackPop()));
        interpreter.EndNewWordCompilation();

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/CONSTANT

CONSTANT

( x "<spaces>name" -- )
Skip leading space delimiters. Parse name delimited by a space. Create a definition for name with the execution semantics defined below.
   
name is referred to as a "constant".
   
name Execution:
   ( -- x )
   Place x on the stack.
   
Rationale:
   Typical use: ... DECIMAL 10 CONSTANT TEN ...
   
Testing:
   T{ 123 CONSTANT X123 -> }T
   T{ X123 -> 123 }T
   T{ : EQU CONSTANT ; -> }T
   T{ X123 EQU Y123 -> }T
   T{ Y123 -> 123 }T

 */
