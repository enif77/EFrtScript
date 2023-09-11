/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class QuestionDoControlWord : IWord, IBranchingWord
{
    public string Name => "?DO";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="currentIndex">The index of this ?DO word inside of a non primitive word.</param>
    public QuestionDoControlWord(int currentIndex)
    {
        _thisIndex = currentIndex;
        _loopIndexIncrement = 0;
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(2);
        interpreter.ReturnStackFree(2);

        var index = interpreter.StackPop().Integer;
        var limit = interpreter.StackPop().Integer;

        interpreter.ReturnStackPush(limit);
        interpreter.ReturnStackPush(index);

        if (limit == index)
        {
            return _loopIndexIncrement;
        }
        else
        {
            return 1;
        }
    }


    public void SetBranchTargetIndex(int branchIndex)
    {
        _loopIndexIncrement = branchIndex - _thisIndex;
    }


    private readonly int _thisIndex;
    private int _loopIndexIncrement;
}
