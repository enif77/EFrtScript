/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;


internal class PlusLoopControlWord : IWord
{
    public string Name => "+LOOP";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="indexIncrement">Offset back to a word following the DO word.</param>
    public PlusLoopControlWord(int indexIncrement)
    {
        _incrementToWordFollowingDo = indexIncrement;
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.ReturnStackExpect(2);
        interpreter.StackExpect(1);

        var index = interpreter.ReturnStackPop().Integer;
        var limit = interpreter.ReturnStackPeek().Integer;
        var increment = interpreter.StackPop().Integer;

        index += increment;

        var condition = (increment >= 0)
            ? (index >= limit)
            : (index <= limit);

        // Is the loop limit reached?
        if (condition)
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
            interpreter.ReturnStackPush(index);

            return _incrementToWordFollowingDo;
        }
    }


    /// <summary>
    /// The index of a word following this DO word.
    /// </summary>
    private readonly int _incrementToWordFollowingDo;
}
