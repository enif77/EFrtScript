/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using EFrtScript.Stacks;


/// <summary>
/// Defines an interpreter runtime state.
/// </summary>
public class InterpreterState : IInterpreterState
{
    /// <inheritdoc cref="IInterpreter"/>
    public IDictionary<string, IWord> Words { get; }
    
    /// <inheritdoc cref="IInterpreter"/>
    public IWord? CurrentWord { get; set; }

    /// <inheritdoc cref="IInterpreter"/>
    public ValueStack Stack { get; }
    
    /// <inheritdoc cref="IInterpreter"/>
    public ValueStack ReturnStack { get; }
    
    /// <inheritdoc cref="IInterpreter"/>
    public ExceptionStack ExceptionStack { get; }
    
    /// <inheritdoc cref="IInterpreter"/>
    public InputSourceStack InputSourceStack { get; }
    
    /// <inheritdoc cref="IInterpreter"/>
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


    /// <inheritdoc cref="IInterpreter"/>
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
