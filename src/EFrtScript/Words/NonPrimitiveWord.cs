/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Words;


internal class NonPrimitiveWord : INonPrimitiveWord
{
    /// <inheritdoc/>
    public string Name { get; }
    
    /// <inheritdoc/>
    public bool IsImmediate { get; private set; }

    /// <inheritdoc/>
    public int ExecutionToken { get; set; }

    /// <summary>
    /// Returns the index of the next word, that will be inserted into this word.
    /// </summary>
    public int NextWordIndex => _words.Count;

    /// <summary>
    /// Gets the number of words defined in this word.
    /// </summary>
    public int WordsCount => _words.Count;


    public NonPrimitiveWord(string wordName)
    {
        Name = wordName;
        _words = new List<IWord>();
    }


    /// <summary>
    /// Marks this word as immediate.
    /// </summary>
    public void SetImmediate()
    {
        IsImmediate = true;
    }

    /// <summary>
    /// Returns a word defined at index.
    /// </summary>
    /// <param name="index">An zero based index of a word defined as part of this word body.</param>
    /// <returns>A word defined at index.</returns>
    public IWord GetWord(int index)
    {
        return _words[index];
    }

    /// <summary>
    /// Adds a word to this word definition.
    /// </summary>
    /// <param name="word">A word to be edded to this word.</param>
    /// <returns>Index of the added word.</returns>
    public int AddWord(IWord word)
    {
        _words.Add(word);

        return _words.Count - 1;
    }


    public int Execute(IInterpreter interpreter)
    {
        _executionBreaked = false;
        var index = 0;
        while (index < _words.Count)
        {
            if (interpreter.IsExecutionTerminated)
            {
                break;
            }

            index += interpreter.ExecuteWord(_words[index]);

            // Used by the DoesWord.
            if (_executionBreaked)
            {
                // Recursive words calls need this "kill switch" to be used just once.
                _executionBreaked = false;

                break;
            }
        }

        return 1;
    }


    /// <summary>
    /// Words defining this word.
    /// </summary>
    private readonly IList<IWord> _words;

    /// <summary>
    /// If true, no more words are executed.
    /// </summary>
    private bool _executionBreaked;
}
