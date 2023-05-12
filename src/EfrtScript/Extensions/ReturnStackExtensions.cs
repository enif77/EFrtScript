/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;


/// <summary>
/// Return stack related Interpreter class extensions. 
/// </summary>
public static class ReturnStackExtensions
{
    public static int GetReturnStackDepth(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Count;
    

    public static void ReturnStackClear(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Clear();

    
    public static bool ReturnStackIsEmpty(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Count == 0;

    
    public static IValue ReturnStackPeek(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Peek() ?? throw NullValueNotAllowedInReturnStackException();

    
    public static void ReturnStackPush(this IInterpreter interpreter, IValue v)
    {
        if (v == null) throw new ArgumentNullException(nameof(v));
        
        interpreter.State.ReturnStack.Push(v);
    }

    
    public static void ReturnStackPush(this IInterpreter interpreter, int v)
    {
        ReturnStackPush(interpreter, new IntegerValue(v));
    }
    
    
    public static IValue ReturnStackPop(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Pop() ?? throw NullValueNotAllowedInReturnStackException();
    

    public static void ReturnStackDrop(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Drop();

    
    /// <summary>
    /// Expects N items on the return stack.
    /// Wont return (throws an InterpreterException), if not enough items are on the return stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="expectedItemsCount">The number of stack items expected on the return stack.</param>
    public static void ReturnStackExpect(this IInterpreter interpreter, int expectedItemsCount)
    {
        if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

        if (interpreter.State.ReturnStack.Count < expectedItemsCount)
        {
            interpreter.Throw(-4, "return stack underflow");
        }
    }

    /// <summary>
    /// Expects N free items on the return stack, so N items can be pushed to the return stack.
    /// Wont return (throws an InterpreterException), if there is not enough free items on the return stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="expectedFreeItemsCount">The number of free return stack items expected.</param>
    public static void ReturnStackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
    {
        if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

        if ((interpreter.State.ReturnStack.Count + expectedFreeItemsCount) >= interpreter.State.ReturnStack.Items.Length)
        {
            interpreter.Throw(-3, "return stack overflow");
        }
    }


    private static Exception NullValueNotAllowedInReturnStackException()
        => new NullReferenceException("Null as value should not be stored in the return stack.");
}