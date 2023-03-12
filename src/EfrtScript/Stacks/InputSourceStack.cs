/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


public class InputSourceStack : AStackBase<IInputSource>
{
    public InputSourceStack(int capacity = 16)
        : base(capacity)
    {
    }
}
