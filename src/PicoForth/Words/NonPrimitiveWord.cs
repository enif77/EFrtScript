/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class NonPrimitiveWord : INonPrimitiveWord
{
    public string Name { get; }
    public bool IsImmediate => false;


    public NonPrimitiveWord(string wordName)
    {
        Name = wordName;
        _words = new List<IWord>();
    }


    public int AddWord(IWord word)
    {
        _words.Add(word);

        return _words.Count - 1;
    }


    public int Execute(IInterpreter interpreter)
    {
        foreach (var word in _words)
        {
            word.Execute(interpreter);
        }

        return 1;
    }


    private readonly IList<IWord> _words;
}
