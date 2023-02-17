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


    public InterpreterState()
    {
        Stack = new ValueStack();
        ReturnStack = new ValueStack();
    }


    public void Reset()
    {
        Stack.Clear();
        ReturnStack.Clear();
    }
}