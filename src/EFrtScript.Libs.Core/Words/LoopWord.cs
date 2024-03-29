/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using System;

using EFrtScript.Extensions;


internal class LoopWord : IWord
{
    public string Name => "LOOP";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);
        interpreter.ReturnStackExpect(1);

        var doWordIndex = interpreter.ReturnStackPop().Integer;
        var loopIndex = interpreter.WordBeingDefined!.AddWord(
            new LoopControlWord(
                (doWordIndex + 1) - interpreter.WordBeingDefined.NextWordIndex));  // c + 1 -> index of the word folowing DO/?DO.

        var cWord = interpreter.WordBeingDefined!.GetWord(doWordIndex);
        if (cWord is IBranchingWord)
        {
            ((IBranchingWord)cWord).SetBranchTargetIndex(loopIndex);
        }

        return 1;        
    }
}
