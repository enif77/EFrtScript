/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;


/// <summary>
/// Exception stack related Interpreter class extensions. 
/// </summary>
public static class ExceptionStackExtensions
{
    /// <summary>
    /// Returns the number of items on the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>The number of items on the exception stack.</returns>
    public static int GetExceptionStackDepth(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Count;
    
    /// <summary>
    /// Clears the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    public static void ExceptionStackClear(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Clear();

    /// <summary>
    /// Checks if the exception stack is empty.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>True, if the exceptions stack is empty.</returns>
    public static bool ExceptionStackIsEmpty(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.IsEmpty;
    
    /// <summary>
    /// Returns the top item from the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>The top item from the stack.</returns>
    /// <exception cref="NullReferenceException">Thrown, when a null value is found on the top of the stack.</exception>
    public static ExceptionFrame ExceptionStackPeek(this IInterpreter interpreter)
        => interpreter.State.ExceptionStack.Peek() ?? throw NullValueNotAllowedInExceptionStackException();

    /// <summary>
    /// Pushes the given exception frame to the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="exceptionFrame">An exception frame.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the exceptionFrame attribute is null.</exception>
    public static void ExceptionStackPush(this IInterpreter interpreter, ExceptionFrame exceptionFrame)
    {
        if (exceptionFrame == null) throw new ArgumentNullException(nameof(exceptionFrame));
        
        interpreter.State.ExceptionStack.Push(exceptionFrame);
    }
    
    /// <summary>
    /// Pops the top item from the exception stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>The top item from the exception stack.</returns>
    /// <exception cref="NullReferenceException">Thrown, when a null value is found on the top of the stack.</exception>
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

        var expectedStackCapacity = interpreter.State.ExceptionStack.Count + expectedFreeItemsCount;
        if (expectedStackCapacity > interpreter.State.ExceptionStack.Capacity || expectedStackCapacity < 0)
        {
            interpreter.Throw(-53, "exception stack overflow");
        }
    }

    
    private static NullReferenceException NullValueNotAllowedInExceptionStackException()
        => new("Null as value should not be stored in the exception stack.");
}