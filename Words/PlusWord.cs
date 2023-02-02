using System;


internal class PlusWord : IWord
{
    public string Name => "+";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new IntValue(evaluator.StackPop().Int + evaluator.StackPop().Int));
    }
}
