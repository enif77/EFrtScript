/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class CommentWord : IWord
{
    public string Name => "(";
    public bool IsImmediate => true;
    

    public void Execute(IEvaluator evaluator)
    {
        var c = evaluator.CurrentChar;
        while (c >= 0)
        {
            if (c == ')')
            {
                break;
            }

            c = evaluator.NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A comment end expected");
        }
    }
}
