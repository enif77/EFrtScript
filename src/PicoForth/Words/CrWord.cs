/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class CrWord : IWord
{
    public string Name => "CR";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.WriteLine();
    }
}
