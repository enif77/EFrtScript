/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class PrintStringWord : IWord
{
    public string Name => "S.";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.Write(evaluator.StackPop().String);
    }
}
