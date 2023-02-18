/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Stacks;

public class ExceptionStack : AStackBase<ExceptionFrame>
{
    public ExceptionStack(int capacity = 32)
        : base(capacity)
    {
    }
}
