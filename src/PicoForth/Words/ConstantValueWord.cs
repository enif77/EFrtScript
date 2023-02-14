/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class ConstantValueWord : IWord
{
    public string Name => "LITERAL";
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
        : this(new IntValue(value))
    {
    }
    
    
    public ConstantValueWord(string value)
        : this(new StringValue(value))
    {
    }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.StackPush(_value);

        return 1;
    }


    private readonly IValue _value; 
}
