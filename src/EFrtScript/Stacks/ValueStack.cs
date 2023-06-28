/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Stack of IValue items.
/// </summary>
public class ValueStack : AStackBase<IValue>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">A stack capacity.</param>
    public ValueStack(int capacity = 32)
        : base(capacity)
    {
    }
}
