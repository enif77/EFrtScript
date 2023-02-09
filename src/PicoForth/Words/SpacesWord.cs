/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class SpacesWord : IWord
{
    public string Name => "SPACES";
    public bool IsImmediate => false;
    

    public void Execute(IEvaluator evaluator)
    {
        var count = evaluator.StackPop().Integer;
        for (var i = 0; i < count; i++)
        {
            evaluator.OutputWriter.Write(" ");
        }
    }
}
