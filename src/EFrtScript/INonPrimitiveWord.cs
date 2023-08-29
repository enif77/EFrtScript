/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines a non primitive word. A word composed of other words.
/// </summary>
public interface INonPrimitiveWord : IWord
{
    /// <summary>
    /// An index of the next added word.
    /// </summary>
    int NextWordIndex { get; }


    /// <summary>
    /// Gets a word from the internal list of words at certain index.
    /// </summary>
    /// <returns>A word.</returns>
    IWord GetWord(int index);

    /// <summary>
    /// Adds a new word to the internal list of words.
    /// </summary>
    /// <returns>The index of the word in the internal list of words.</returns>
    int AddWord(IWord word);

    /// <summary>
    /// If called, no more words from this word are executed.
    /// </summary>
    void BreakExecution();
}
