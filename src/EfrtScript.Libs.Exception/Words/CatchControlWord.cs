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
                interpreter.GetRegisteredWord(
                    interpreter.StackPop().String.ToUpperInvariant()));

            // Remove the unused exception frame (nothing failed here).
            _ = interpreter.ExceptionStackPop();

            // Return the OK status.
            interpreter.StackPush(0);

            return index;
        }
        catch (InterpreterException ex)
        {
            CleanExceptionStack(interpreter, exceptionFrame);

            interpreter.StackPush(new ExceptionValue(ex));
        }
        catch (Exception ex)
        {
            CleanExceptionStack(interpreter, exceptionFrame);

            interpreter.StackPush(new ExceptionValue(ex));
        }

        // Go to the word behind us.
        return 1;
    }
    
    private readonly IWord _parentWord;
    private readonly int _nextWordIndex;


    private void CleanExceptionStack(IInterpreter interpreter, ExceptionFrame exceptionFrame)
    {
        if (interpreter.ExceptionStackIsEmpty() == false && interpreter.ExceptionStackPeek() == exceptionFrame)
        {
            _ = interpreter.ExceptionStackPop();
        }
    }
}
