/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Stores values.
/// </summary>
public class Heap : AStackBase<IValue>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">A heap capacity.</param>
    public Heap(int capacity = 64)
        : base(capacity)
    {
    }
    
    
    #region heap

    /// <summary>
    /// Stores a value to a cell at a certain address.
    /// </summary>
    /// <param name="address">An address of a heap cell.</param>
    /// <param name="value">A value.</param>
    public void Store(int address, IValue value)
        => Items[address] = value;

    /// <summary>
    /// Gets a value from a cell at a certain address.
    /// </summary>
    /// <param name="address">An address of a heap cell.</param>
    /// <returns>A value.</returns>
    public IValue? Fetch(int address)
        => Items[address];

    #endregion
}
