internal class ReadStringLitWord : IWord
{
    public string Name => "S\"";
    

    public void Execute(IEvaluator evaluator)
    {
        evaluator.StackPush(new StringValue(evaluator.ReadStringFromSource()));
    }
}
