/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


/// <summary>
/// A word that is defining begin-until loop end.
/// </summary>
internal class UntilControlWord : IWord
{
    public string Name => "UNTIL";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexIncrement">Offset back to a word following the BEGIN word.</param>
    public UntilControlWord(int indexIncrement)
    {
        _incrementToWordFollowingBegin = indexIncrement;
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackExpect(1);

        // (flag -- )
        if (interpreter.StackPop().Boolean == false)
        {
            // The flag is FALSE - repeat the loop.
            return _incrementToWordFollowingBegin;
        }

        // The flag is TRUE, end the loop.
        return 1;
    }


    /// <summary>
    /// The index of a word following this DO word.
    /// </summary>
    private readonly int _incrementToWordFollowingBegin;
}
