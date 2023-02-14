/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Values;


/// <summary>
/// A word that is defining loop end.
/// </summary>
internal class LoopControlWord : IWord
{
    public string Name => "LOOP";
    public bool IsImmediate => false;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexIncrement">Offset back to a word following the DO word.</param>
    public LoopControlWord(int indexIncrement)
    {
        _incrementToWordFollowingDo = indexIncrement;
    }


    public int Execute(IInterpreter interpreter)
    {
        var index = interpreter.ReturnStackPop().Integer;
        var limit = interpreter.ReturnStackPeek().Integer;

        index += 1;

        // Is the loop limit reached?
        if (index >= limit)
        {
            // Yes we're done. Pop limit off of variable stack and
            // return a positive one instruction increment.
            interpreter.ReturnStackPop();

            return 1;

        }
        else
        {
            // Loop index has not been reached. Push new index value
            // and return negative instruction increment to cause
            // control to return to word immediately following the DO word.
            interpreter.ReturnStackPush(new IntValue(index));

            return _incrementToWordFollowingDo;
        }
    }


    /// <summary>
    /// The index of a word following this DO word.
    /// </summary>
    private readonly int _incrementToWordFollowingDo;
}
