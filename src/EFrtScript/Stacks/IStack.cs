/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Defines a stack.
/// </summary>
public interface IStack
{
    /// <summary>
    /// The number of items on this stack.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// The capacity of this stack.
    /// </summary>
    int Capacity { get; }

    /// <summary>
    /// Is true, when no items are in this stack.
    /// </summary>
    bool IsEmpty { get; }

    
    /// <summary>
    /// Removes all values from the stack.
    /// </summary>
    void Clear();
}
