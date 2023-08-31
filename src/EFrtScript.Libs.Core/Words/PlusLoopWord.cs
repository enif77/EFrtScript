/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class PlusLoopWord : IWord
{
    public string Name => "+LOOP";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);

        var cWordIndex = interpreter.ReturnStackPop().Integer;

        var loopIndex = interpreter.WordBeingDefined!.AddWord(
            new PlusLoopControlWord(
                cWordIndex + 1 - interpreter.WordBeingDefined!.NextWordIndex));  // c + 1 -> index of the word following DO/?DO.

        var cWord = interpreter.WordBeingDefined.GetWord(cWordIndex);
        if (cWord is IBranchingWord)
        {
            ((IBranchingWord)cWord).SetBranchTargetIndex(loopIndex);
        }

        // TODO: What to do, if the cWord is not a branching word?

        return 1;
    }
}

/*

https://forth-standard.org/standard/core/PlusLOOP

+LOOP

Add n to the loop index. If the loop index did not cross the boundary between the loop limit
minus one and the loop limit, continue execution at the beginning of the loop. Otherwise,
discard the current loop control parameters and continue execution immediately following the loop.
Compilation: [index of the word following DO/?DO -- ], runtime: (n -- )

Example:

: GD2 DO I DUP . -1 +LOOP CR ;

1 4 GD2 -> 4 3 2
-1 2 GD2 -> 2 1 0

*/
