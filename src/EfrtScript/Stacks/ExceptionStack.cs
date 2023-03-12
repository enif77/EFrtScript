/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Stacks;

public class ExceptionStack : AStackBase<ExceptionFrame>
{
    public ExceptionStack(int capacity = 32)
        : base(capacity)
    {
    }
}
