/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SpaceWord : IWord
{
    public string Name => "SPACE";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.Write(" ");
    }
}
