/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using EFrtScript.Stacks;


/// <summary>
/// Defines an interpreter runtime state.
/// </summary>
public class InterpreterState : IInterpreterState
{
    /// <inheritdoc/>
    public IDictionary<string, IWord> Words { get; }
    
    /// <inheritdoc/>
    public IWord? CurrentWord { get; set; }

    /// <inheritdoc/>
    public ValueStack Stack { get; }
    
    /// <inheritdoc/>
    public ValueStack ReturnStack { get; }
    
    /// <inheritdoc/>
    public ExceptionStack ExceptionStack { get; }
    
    /// <inheritdoc/>
    public InputSourceStack InputSourceStack { get; }
    
    /// <inheritdoc/>
    public Heap Heap { get; }


    /// <summary>
    /// Constructor.
    /// </summary>
    public InterpreterState()
    {
        Words = new Dictionary<string, IWord>();
        Stack = new ValueStack();
        ReturnStack = new ValueStack();
        ExceptionStack = new ExceptionStack();
        InputSourceStack = new InputSourceStack();
        Heap = new Heap();
    }


    /// <inheritdoc/>
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
