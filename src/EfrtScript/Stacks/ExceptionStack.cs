/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;


/// <summary>
/// Stack of exception frames.
/// </summary>
public class ExceptionStack : AStackBase<ExceptionFrame>
{
    /// <summary>
    /// Creates a new instance of the ExceptionStack class.
    /// </summary>
    /// <param name="capacity">The stack capacity.</param>
    public ExceptionStack(int capacity = 32)
        : base(capacity)
    {
    }
}
