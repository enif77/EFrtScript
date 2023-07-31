/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Keeps the list of known words.
/// </summary>
public class WordsList : IWordsList
{
    /// <inheritdoc/>
    public IEnumerable<IWord> DefinedWords
    {
        get
        {
            var wordsList = new List<IWord>();

            foreach (var w in _wordsDic.Keys)
            {
                wordsList.Add(Get(w));
            }

            return wordsList;
        }
    }

    /// <inheritdoc/>
    public IEnumerable<IWord> WordsHistory
        => new List<IWord>(_wordsHistory);


    /// <summary>
    /// Constructor.
    /// </summary>
    public WordsList()
    {
        _wordsDic = new Dictionary<string, IList<IWord>>();
        _wordsHistory = new List<IWord>();
    }


    /// <inheritdoc/>
    public void Clear()
    {
        _wordsDic.Clear();
        _wordsHistory.Clear();
    }

    /// <inheritdoc/>
    public void Forget(string wordName)
    {
        // NOTE: A wordIndex is actually its execution token.
        var wordIndex = _wordsHistory.LastIndexOf(Get(wordName));
        var wordsToBeRemovedList = new List<IWord>();
        for (var i = wordIndex; i < _wordsHistory.Count; i++)
        {
            wordsToBeRemovedList.Add(_wordsHistory[i]);
        }

        foreach (var w in wordsToBeRemovedList)
        {
            Remove(w.Name);
        }
    }

    /// <inheritdoc/>
    public IWord Get(string wordName)
    {
        return _wordsDic[wordName].Last();
    }

    /// <inheritdoc/>
    public IWord Get(int executionToken)
    {
        return _wordsHistory[executionToken];
    }

    /// <inheritdoc/>
    public bool IsDefined(string wordName)
    {
        return _wordsDic.ContainsKey(wordName);
    }

    /// <inheritdoc/>
    public bool IsDefined(int executionToken)
    {
        return executionToken >= 0 && executionToken < _wordsHistory.Count;
    }


    /// <inheritdoc/>
    public void Add(IWord word)
    {
        if (IsDefined(word.Name) == false)
        {
            _wordsDic.Add(word.Name, new List<IWord>());
        }

        // Add the word to the list of words.
        _wordsDic[word.Name].Add(word);
        _wordsHistory.Add(word);

        // Set the execution token to the word.
        word.ExecutionToken = _wordsHistory.Count - 1;
    }

    /// <inheritdoc/>
    public void Remove(string wordName)
    {
        var wordDefinitionsList = _wordsDic[wordName];
        var wordTobeRemoved = wordDefinitionsList.Last();
        _wordsHistory.Remove(wordTobeRemoved);
        wordDefinitionsList.RemoveAt(wordDefinitionsList.Count - 1);
        if (wordDefinitionsList.Count == 0)
        {
            _wordsDic.Remove(wordName);
        }
        wordTobeRemoved.ExecutionToken = -1;
    }
    
    /// <inheritdoc/>
    public override string ToString()
    {
        var nextWord = false;
        var sb = new StringBuilder();
        var knownWords = new Dictionary<string, int>(_wordsDic.Count);
        for (var i = _wordsHistory.Count - 1; i >= 0; i--)
        {
            var word = _wordsHistory[i];
            if (knownWords.ContainsKey(word.Name))
            {
                continue;
            }

            if (nextWord)
            {
                sb.Append(' ');
            }
            else
            {
                nextWord = true;
            }

            sb.Append(word.Name);
            knownWords.Add(word.Name, i);
        }

        return sb.ToString();
    }


    private readonly Dictionary<string, IList<IWord>> _wordsDic;
    private readonly List<IWord> _wordsHistory;
}
