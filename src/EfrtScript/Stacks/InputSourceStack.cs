/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Stack of IInputSource instances.
/// </summary>
public class InputSourceStack : AStackBase<IInputSource>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="capacity">A stack capacity.</param>
    public InputSourceStack(int capacity = 16)
        : base(capacity)
    {
    }
}
