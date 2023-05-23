/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using EFrtScript.Stacks;


/// <summary>
/// An interpreter state.
/// </summary>
public class InterpreterState : IInterpreterState
{
    public IDictionary<string, IWord> Words { get; }
    public IWord? CurrentWord { get; set; }

    public ValueStack Stack { get; }
    public ValueStack ReturnStack { get; }
    public ExceptionStack ExceptionStack { get; }
    public InputSourceStack InputSourceStack { get; }
    public Heap Heap { get; }


    public InterpreterState()
    {
        Words = new Dictionary<string, IWord>();
        Stack = new ValueStack();
        ReturnStack = new ValueStack();
        ExceptionStack = new ExceptionStack();
        InputSourceStack = new InputSourceStack();
        Heap = new Heap();
    }


    public void Reset()
    {
        Words.Clear();
        CurrentWord = null;
        Stack.Clear();
        ReturnStack.Clear();
        ExceptionStack.Clear();
        InputSourceStack.Clear();
        Heap.Clear();
    }
}