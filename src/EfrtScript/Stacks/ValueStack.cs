/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


public class ValueStack : AStackBase<IValue>
{
    public ValueStack(int capacity = 32)
        : base(capacity)
    {
    }
}
