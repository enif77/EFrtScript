/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Values;

namespace EFrtScript.Extensions;


/// <summary>
/// Interpreter class extensions. 
/// </summary>
public static class InterpreterExtensions
{
    #region stack

    public static int GetStackDepth(this IInterpreter interpreter)
        => interpreter.State.Stack.Count;
    
    
    public static void StackClear(this IInterpreter interpreter)
        => interpreter.State.Stack.Clear();

    
    public static bool StackIsEmpty(this IInterpreter interpreter)
        => interpreter.State.Stack.IsEmpty;

    
    public static IValue StackPeek(this IInterpreter interpreter)
        => interpreter.State.Stack.Peek() ?? throw NullValueNotAllowedInStackException();

    
    public static void StackPush(this IInterpreter interpreter, IValue v)
    {
        if (v == null) throw new ArgumentNullException(nameof(v));
        
        interpreter.State.Stack.Push(v);
    }
    
    
    public static void StackPush(this IInterpreter interpreter, int v)
    {
        StackPush(interpreter, new IntegerValue(v));
    }
    
    
    public static void StackPush(this IInterpreter interpreter, string v)
    {
        StackPush(interpreter, new StringValue(v));
    }
    
    
    public static IValue StackPop(this IInterpreter interpreter)
        => interpreter.State.Stack.Pop() ?? throw NullValueNotAllowedInStackException();
    
    /// <summary>
    /// Expects N items on the stack.
    /// Wont return (throws an InterpreterException), if not enough items are on the stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="expectedItemsCount">The number of stack items expected on the stack.</param>
    public static void StackExpect(this IInterpreter interpreter, int expectedItemsCount)
    {
        if (expectedItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedItemsCount));

        if (interpreter.State.Stack.Count < expectedItemsCount)
        {
            interpreter.Throw(-4, "stack underflow");
        }
    }

    /// <summary>
    /// Expects N free items on the stack, so N items can be pushed to the stack.
    /// Wont return (throws an InterpreterException), if there is not enough free items on the stack.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="expectedFreeItemsCount">The number of free stack items expected.</param>
    public static void StackFree(this IInterpreter interpreter, int expectedFreeItemsCount)
    {
        if (expectedFreeItemsCount < 0) throw new ArgumentOutOfRangeException(nameof(expectedFreeItemsCount));

        if ((interpreter.State.Stack.Count + expectedFreeItemsCount) >= interpreter.State.Stack.Items.Length)
        {
            interpreter.Throw(-3, "stack overflow");
        }
    }

    
    private static Exception NullValueNotAllowedInStackException()
        => new NullReferenceException("Null as value should not be stored in the stack.");
    
    #endregion
    
    
    #region return stack
    
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

    #endregion
    

    #region heap
    
    public static void HeapStore(this IInterpreter interpreter, int address, IValue value)
        => interpreter.State.Heap.Store(address, value);


    public static IValue HeapFetch(this IInterpreter interpreter, int address)
        => interpreter.State.Heap.Fetch(address) ?? throw new NullReferenceException("Null as value should not be stored in the heap.");

    #endregion
}