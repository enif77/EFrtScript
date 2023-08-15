/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class UntilWord : IWord
{
    public string Name => "UNTIL";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);

        interpreter.WordBeingDefined!.AddWord(
            new UntilControlWord(
                interpreter.ReturnStackPop().Integer - interpreter.WordBeingDefined!.NextWordIndex));

            return 1;     
    }
}

/*

https://forth-standard.org/standard/core/UNTIL

UNTIL

(R: dest -- )
( flag --)
If all bits of x are zero, continue execution at the location specified by dest.

*/