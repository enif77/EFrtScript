/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;


/// <summary>
/// Exception stack related Interpreter class extensions. 
/// </summary>
public static class ExceptionStackExtensions
{
    public static int GetExceptionStackDepth(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Count;
    
    
    public static void ExceptionStackClear(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Clear();

    
    public static bool ExceptionStackIsEmpty(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.IsEmpty;

    
    public static ExceptionFrame ExceptionStackPeek(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Peek() ?? throw NullValueNotAllowedInExceptionStackException();

    
    public static void ExceptionStackPush(this IInterpreter interpreter, ExceptionFrame exceptionFrame)
    {
        if (exceptionFrame == null) throw new ArgumentNullException(nameof(exceptionFrame));
        
        interpreter.State.ExceptionStack.Push(exceptionFrame);
    }
    
    
    public static ExceptionFrame ExceptionStackPop(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Pop() ?? throw NullValueNotAllowedInExceptionStackException();
    
    /// <summary>
    /// Expects N free items on the exception stack, so N items can be pushed to the exception stack.
    /// Wont return (throws an InterpreterException), if there is not enough free items on the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="expectedFreeItemsCount">The number of free stack items expected.</param>
    public static void ExceptionStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
    {
        if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

        if ((interpreter.State.ExceptionStack.Count + expectedFreeItemsCount) >= interpreter.State.ExceptionStack.Items.Length)
        {
            interpreter.Throw(-53, "exception stack overflow");
        }
    }

    
    private static Exception NullValueNotAllowedInExceptionStackException()
        => new NullReferenceException("Null as value should not be stored in the exception stack.");
}