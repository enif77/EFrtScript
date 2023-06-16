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

    /// <inheritdoc cref="IInterpreter"/>
    public void Store(int address, IValue value)
        => Items[address] = value;

    /// <inheritdoc cref="IInterpreter"/>
    public IValue? Fetch(int address)
        => Items[address];

    #endregion
}
