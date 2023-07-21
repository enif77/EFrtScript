/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;


/// <summary>
/// Stack related Interpreter class extensions. 
/// </summary>
public static class StackExtensions
{
    /// <summary>
    /// Returns the number of items on the stack.
    /// </summary>
    public static int GetStackDepth(this IInterpreter interpreter)
        => interpreter.State.Stack.Count;
    
    /// <summary>
    /// Removes all items from the stack.
    /// </summary>
    public static void StackClear(this IInterpreter interpreter)
        => interpreter.State.Stack.Clear();

    /// <summary>
    /// Returns true if the stack is empty.
    /// </summary>
    public static bool StackIsEmpty(this IInterpreter interpreter)
        => interpreter.State.Stack.IsEmpty;

    /// <summary>
    /// Returns the top item from the stack.
    /// </summary>
    public static IValue StackPeek(this IInterpreter interpreter)
        => interpreter.State.Stack.Peek() ?? throw NullValueNotAllowedInStackException();

    /// <summary>
    /// Pushes the given value to the stack.
    /// </summary>
    public static void StackPush(this IInterpreter interpreter, IValue v)
    {
        if (v == null) throw new ArgumentNullException(nameof(v));
        
        interpreter.State.Stack.Push(v);
    }
    
    /// <summary>
    /// Pushes the given boolean value to the stack.
    /// </summary>
    public static void StackPush(this IInterpreter interpreter, bool v)
    {
        StackPush(interpreter, new IntegerValue(v ? -1 : 0));
    }
    
    /// <summary>
    /// Pushes the given integer value to the stack.
    /// </summary>
    public static void StackPush(this IInterpreter interpreter, int v)
    {
        StackPush(interpreter, new IntegerValue(v));
    }

    /// <summary>
    /// Pushes the given double value to the stack.
    /// </summary>
    public static void StackPush(this IInterpreter interpreter, double v)
    {
        StackPush(interpreter, new FloatValue(v));
    }
    
    /// <summary>
    /// Pushes the given string value to the stack.
    /// </summary>
    public static void StackPush(this IInterpreter interpreter, string v)
    {
        StackPush(interpreter, new StringValue(v));
    }
    
    /// <summary>
    /// Returns the top item from the stack and removes it from the stack.
    /// </summary>
    public static IValue StackPop(this IInterpreter interpreter)
        => interpreter.State.Stack.Pop() ?? throw NullValueNotAllowedInStackException();
    
    /// <summary>
    /// Returns the top item from the stack as integer and removes it from the stack.
    /// </summary>
    public static int StackPopInteger(this IInterpreter interpreter)
    {
        return interpreter.ConvertToInteger(
            interpreter.State.Stack.Pop() ?? throw NullValueNotAllowedInStackException()
            ).Integer;
    }

    /// <summary>
    /// Removes top item from the stack.
    /// </summary>
    public static void StackDrop(this IInterpreter interpreter)
        => interpreter.State.Stack.Drop();
    
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

        if ((interpreter.State.Stack.Count + expectedFreeItemsCount) >= interpreter.State.Stack.Capacity)
        {
            interpreter.Throw(-3, "stack overflow");
        }
    }

    
    private static Exception NullValueNotAllowedInStackException()
        => new NullReferenceException("Null as value should not be stored in the stack.");
}