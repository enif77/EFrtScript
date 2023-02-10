/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class NonPrimitiveWord : INonPrimitiveWord
{
    public string Name { get; }
    public bool IsImmediate => false;
    public bool IsControlWord => false;
    

    public NonPrimitiveWord(string wordName)
    {
        Name = wordName;
        _words = new List<IWord>();
    }


    public void AddWord(IWord word)
    {
        _words.Add(word);
    }


    public void Execute(IEvaluator evaluator)
    {
        foreach (var word in _words)
        {
            word.Execute(evaluator);
        }
    }


    private readonly IList<IWord> _words;
}
