/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Words;

using System;

using EFrtScript.Extensions;
using EFrtScript.Libs.Exception.Values;


/// <summary>
/// A word catching an exception.
/// </summary>
internal class CatchControlWord : IWord
{
    public string Name => "CATCH";
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }
    
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="parentWord">A word, that is executing this CATCH word.</param>
    /// <param name="nextWordIndex">An index of a word following this CATCH word.</param>
    public CatchControlWord(IWord parentWord, int nextWordIndex)
    {
        _parentWord = parentWord;
        _nextWordIndex = nextWordIndex;
    }
    

    public int Execute(IInterpreter interpreter)
    {
        // A word name to execute expected.
        interpreter.StackExpect(1);

        // A place for a new exception frame must be available.
        interpreter.ExceptionStackFree(1);

        // Remove the execution token from the stact so we'll return to a correct place when an exception occurs.
        var executionToken = interpreter.StackPop().Integer;

        var exceptionFrame = new ExceptionFrame()
        {
            StackTop = interpreter.State.Stack.Top,
            ReturnStackTop = interpreter.State.ReturnStack.Top,
            InputSourceStackTop = interpreter.State.InputSourceStack.Top,
            ExecutingWord = _parentWord,
            NextWordIndex = _nextWordIndex
        };

        interpreter.ExceptionStackPush(exceptionFrame);

        try
        {
            // Execute the word.
            var index = interpreter.ExecuteWord(
                interpreter.GetRegisteredWord(executionToken));

            // Remove the unused exception frame (nothing failed here).
            _ = interpreter.ExceptionStackPop();

            // Return the OK status.
            interpreter.StackPush(0);

            return index;
        }
        catch (Exception ex)
        {
            if (interpreter.ExceptionStackIsEmpty() || interpreter.ExceptionStackPeek() != exceptionFrame)
            {
                throw new InvalidOperationException("This CATCH word created exception frame not found.");
            }
        
            // Remove the exception frame.
            _ = interpreter.State.ExceptionStack.Pop();

            // Restore the previous execution state.
            interpreter.State.Stack.Top = exceptionFrame.StackTop;
            interpreter.State.ReturnStack.Top = exceptionFrame.ReturnStackTop;
            interpreter.State.InputSourceStack.Top = exceptionFrame.InputSourceStackTop;
            interpreter.State.CurrentWord = exceptionFrame.ExecutingWord ?? throw new InvalidOperationException("Exception frame without a executing word reference.");

            // Push the exception to the stack.
            interpreter.StackPush(new ExceptionValue(ex));

            // Push "yes we had an exception" flag to the stack.
            interpreter.StackPush(-1);
        }

        // Go to the word behind us.
        return 1;
    }
    
    private readonly IWord _parentWord;
    private readonly int _nextWordIndex;
}
