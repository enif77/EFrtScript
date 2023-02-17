/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using PicoForth.Stacks;


/// <summary>
/// An interpreter stack.
/// </summary>
public class InterpreterState : IInterpreterState
{
    public ValueStack Stack { get; }
    public ValueStack ReturnStack { get; }
    public Heap Heap { get; }


    public InterpreterState()
    {
        Stack = new ValueStack();
        ReturnStack = new ValueStack();
        Heap = new Heap();
    }


    public void Reset()
    {
        Stack.Clear();
        ReturnStack.Clear();
        Heap.Clear();
    }
}