using System;
using System.Text;


internal class PrintStringWord : IWord
{
    public string Name => "S.";
    

    public void Execute(IEvaluator evaluator)
    {
        Console.Write(evaluator.StackPop().String);
    }
}
