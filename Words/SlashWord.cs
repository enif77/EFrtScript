using System;


internal class SlashWord : IWord
{
    public string Name => "/";
    

    public void Execute(IEvaluator evaluator)
    {
        var b = evaluator.StackPop().Int;
        evaluator.StackPush(new IntValue(evaluator.StackPop().Int / b));
    }
}
