using System;


internal class DotWord : IWord
{
    public string Name => ".";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.Write(evaluator.StackPop().Int);
    }
}
