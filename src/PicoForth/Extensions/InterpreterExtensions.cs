/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

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
    
    
    public static void StackPush(this IInterpreter interpreter, int v)
    {
        StackPush(interpreter, new IntValue(v));
    }
    
    
    public static void StackPush(this IInterpreter interpreter, string v)
    {
        StackPush(interpreter, new StringValue(v));
    }
    
    
    public static IValue StackPop(this IInterpreter interpreter)
        => interpreter.State.Stack.Pop() ?? throw NullValueNotAllowedInStackException();
    
    
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
        ReturnStackPush(interpreter, new IntValue(v));
    }
    
    
    public static IValue ReturnStackPop(this IInterpreter interpreter)
        => interpreter.State.ReturnStack.Pop() ?? throw NullValueNotAllowedInReturnStackException();
    
    
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