/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Stacks;

namespace EFrtScript;

using EFrtScript.Stacks;


/// <summary>
/// An interpreter state.
/// </summary>
public class InterpreterState : IInterpreterState
{
    public ValueStack Stack { get; }
    public ValueStack ReturnStack { get; }
    public ExceptionStack ExceptionStack { get; }
    public InputSourceStack InputSourceStack { get; }
    public Heap Heap { get; }


    public InterpreterState()
    {
        Stack = new ValueStack();
        ReturnStack = new ValueStack();
        ExceptionStack = new ExceptionStack();
        InputSourceStack = new InputSourceStack();
        Heap = new Heap();
    }


    public void Reset()
    {
        Stack.Clear();
        ReturnStack.Clear();
        ExceptionStack.Clear();
        InputSourceStack.Clear();
        Heap.Clear();
    }
}