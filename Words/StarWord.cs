using System;


internal class StarWord : IWord
{
    public string Name => "*";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new IntValue(evaluator.StackPop().Int * evaluator.StackPop().Int));
    }
}
