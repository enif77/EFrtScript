/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


public class Heap : AStackBase<IValue>
{
    public Heap(int capacity = 64)
        : base(capacity)
    {
    }
    
    
    #region heap

    public void Store(int address, IValue value)
        => Items[address] = value;


    public IValue? Fetch(int address)
        => Items[address];

    #endregion
}
