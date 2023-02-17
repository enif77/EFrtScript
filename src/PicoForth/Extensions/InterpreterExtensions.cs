/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Extensions;

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
    
    
    public static IValue StackPop(this IInterpreter interpreter)
        => interpreter.State.Stack.Pop() ?? throw NullValueNotAllowedInStackException();
    
    
    private static Exception NullValueNotAllowedInStackException()
        => new NullReferenceException("Null as value should not be stored in the Stack.");
    
    #endregion
}