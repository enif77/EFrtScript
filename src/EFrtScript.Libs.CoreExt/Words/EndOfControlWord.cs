/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;


internal class EndOfControlWord : IWord
{
    public string Name => "ENDOF";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="currentIndex">The index of this ENDOF word inside the word it belongs to.</param>
    public EndOfControlWord(int currentIndex)
    {
        _thisIndex = currentIndex;
        _endCaseIndexIncrement = 0;
    }
    
    
    /// <summary>
    /// Called by the word ENDCASE. It inserts here its index in the word it belongs to.
    /// </summary>
    /// <param name="endCaseIndex">The ENDCASE index.</param>
    public void SetEndCaseIndex(int endCaseIndex)
    {
        _endCaseIndexIncrement = endCaseIndex - _thisIndex;
    }
    

    public int Execute(IInterpreter interpreter)
    {
        // Advance to the ENDCASE word.
        return _endCaseIndexIncrement + 1;
    }
    
    
    private readonly int _thisIndex;
    private int _endCaseIndexIncrement;
}
