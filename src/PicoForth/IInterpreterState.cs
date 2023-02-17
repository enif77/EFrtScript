/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using PicoForth.Stacks;


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
    /// Heap.
    /// </summary>
    Heap Heap { get; }
    
    
    /// <summary>
    /// Cleans up this state.
    /// </summary>
    void Reset();
}
