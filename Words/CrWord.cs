using System;


internal class CrWord : IWord
{
    public string Name => "CR";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.WriteLine();
    }
}
