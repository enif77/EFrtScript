public interface IWord
{
    string Name { get; }

    void Execute(IEvaluator evaluator);
}
