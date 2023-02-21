/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


internal class LoopWord : IWord
{
    public string Name => "LOOP";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("LOOP outside a new word definition.");
        }

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
