/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Values;


/// <summary>
/// Holds a reference to a word.
/// </summary>
internal class WordReferenceValue : IValue
{
    /// <summary>
    /// Unsupported conversion to a boolean value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Word reference cannot be casted to Boolean.</exception>
    public bool Boolean => throw new InvalidOperationException("A word reference cannot be casted to a boolean value.");
    
    /// <summary>
    /// Unsupported conversion to an bnteger value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Word reference cannot be casted to Integer.</exception>
    public int Integer => throw new InvalidOperationException("A word reference cannot be casted to an integer value.");
    
    /// <summary>
    /// Returns the name of the referenced word.
    /// </summary>
    public string String => Word.Name;
    
    /// <summary>
    /// A word.
    /// </summary>
    public IWord Word { get; }

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="word">A word to be referenced.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the word argument is null.</exception>
    public WordReferenceValue(IWord word)
    {
        Word = word ?? throw new ArgumentNullException(nameof(word));
    }
}
