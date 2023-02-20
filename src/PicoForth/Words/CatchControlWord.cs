/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


/// <summary>
/// A word catching an exception.
/// </summary>
internal class CatchControlWord : IWord
{
    public string Name => "CATCH";
    public bool IsImmediate => false;
    
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="parentWord">A word, that is executing this CATCH word.</param>
    /// <param name="nextWordIndex">An index of a word following this CATCH word..</param>
    public CatchControlWord(IWord parentWord, int nextWordIndex)
    {
        _parentWord = parentWord;
        _nextWordIndex = nextWordIndex;
    }
    

    public int Execute(IInterpreter interpreter)
    {
        // Exception stack free.
        if ((interpreter.State.ExceptionStack.Count + 1) >= interpreter.State.ExceptionStack.Items.Length)
        {
            throw new InterpreterException(-3, "Exception stack overflow.");
        }

        //Interpreter.StackExpect(1);

        var exceptionFrame = new ExceptionFrame()
        {
            StackTop = interpreter.State.Stack.Top,
            ReturnStackTop = interpreter.State.ReturnStack.Top,
            //InputSourceStackTop = Interpreter.State.InputSourceStack.Top,
            ExecutingWord = _parentWord,
            NextWordIndex = _nextWordIndex
        };

        interpreter.State.ExceptionStack.Push(exceptionFrame);

        try
        {
            // Execute the word.
            var index = interpreter.ExecuteWord(
                interpreter.GetRegisteredWord(
                    interpreter.StackPop().String.ToUpperInvariant()));

            // Remove the unused exception frame (nothing failed here).
            interpreter.State.ExceptionStack.Pop();

            // Return the OK status.
            interpreter.StackPush(new IntValue(0));

            return index;
        }
        catch (Exception)
        {
            // Clean up the mess, if needed.
            if (interpreter.State.ExceptionStack.IsEmpty == false && interpreter.State.ExceptionStack.Peek() == exceptionFrame)
            {
                interpreter.State.ExceptionStack.Pop();
            }

            // TODO: What to do with the exception?

            // Go to the word behind us.
            return 1;
        }
    }
    
    private readonly IWord _parentWord;
    private readonly int _nextWordIndex;
}
