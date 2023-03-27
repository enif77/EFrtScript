/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Words;

using EFrtScript.Extensions;
using EFrtScript.Values;


public class ConstantValueWord : IWord
{
    public string Name => $"LITERAL({_value.String})";
    public bool IsImmediate => false;


    private ConstantValueWord(IValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    
    public ConstantValueWord(bool value)
        : this(value ? -1 : 0)
    {
    }
    
    
    public ConstantValueWord(int value)
        : this(new IntegerValue(value))
    {
    }


    public ConstantValueWord(double value)
        : this(new FloatValue(value))
    {
    }
    
    
    public ConstantValueWord(string value)
        : this(new StringValue(value))
    {
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackFree(1);

        interpreter.StackPush(_value);

        return 1;
    }

    private readonly IValue _value; 
}
