/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word that is defining condition in the BEGIN-WHILE-REPEAT loop.
/// </summary>
internal class WhileControlWord : IWord
{
    public string Name => "WHILE";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="currentIndex">The index of this WHILE word inside of a non primitive word.</param>
    public WhileControlWord(int currentIndex)
    {
        _thisIndex = currentIndex;
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        if (interpreter.StackPop().Boolean)
        {
            // The flag is true, advance instruction index by one to loop body.
            return 1;
        }

        // The flag is false, advance to a word behind the REPEAT word.
        return _repeatIndexIncrement + 1;
    }


    /// <summary>
    /// Called by the word REPEAT. It inserts its index inside a word its defined in.
    /// </summary>
    /// <param name="repeatIndex">The REPEAT word index.</param>
    public void SetRepeatIndex(int repeatIndex)
    {
        _repeatIndexIncrement = repeatIndex - _thisIndex;
    }

    
    private readonly int _thisIndex;
    private int _repeatIndexIncrement;
}
