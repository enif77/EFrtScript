/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Values;

namespace PicoForth.Words;


internal class IntegerConstantWord : IWord
{
    public string Name => "LITERAL";
    public bool IsImmediate => false;
    

    public IntegerConstantWord(int value)
    {
        _value = new IntValue(value);
    }


    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(_value);
    }


    private readonly IValue _value; 
}
