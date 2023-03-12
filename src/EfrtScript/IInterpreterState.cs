/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Stacks;

namespace EFrtScript;

using EFrtScript.Stacks;


/// <summary>
/// Defines an interpreter runtime state.
/// </summary>
public interface IInterpreterState
{
    /// <summary>
    /// The main data stack.
    /// </summary>
    ValueStack Stack { get; }
    
    /// <summary>
    /// Return stack.
    /// </summary>
    ValueStack ReturnStack { get; }
    
    /// <summary>
    /// Exception stack.
    /// </summary>
    ExceptionStack ExceptionStack { get; }

    /// <summary>
    /// Input sources stack.
    /// </summary>
    InputSourceStack InputSourceStack { get; }

    /// <summary>
    /// Heap.
    /// </summary>
    Heap Heap { get; }
    
    
    /// <summary>
    /// Cleans up this state.
    /// </summary>
    void Reset();
}
