/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;

using PicoForth.Extensions;
using PicoForth.Values;


public class ConstantValueWord : IWord
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
    
    
    public ConstantValueWord(IWord word)
        : this(new WordReferenceValue(word))
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
