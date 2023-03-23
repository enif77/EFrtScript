/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;
using EFrtScript.Words;


/// <summary>
/// Stack related Interpreter class extensions. 
/// </summary>
public static class StackExtensions
{
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


    public static void StackPush(this IInterpreter interpreter, double v)
    {
        StackPush(interpreter, new RealValue(v));
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
}