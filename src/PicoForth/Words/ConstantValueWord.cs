/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class ConstantValueWord : IWord
{
    public string Name => "LITERAL";
    public bool IsImmediate => false;
    public bool IsControlWord => true;


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


    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(_value);
    }


    private readonly IValue _value; 
}
