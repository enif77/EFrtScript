/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

/// <summary>
/// Defines a library of words.
/// </summary>
public interface IWordsLibrary
{
    /// <summary>
    /// A name of a words library.
    /// </summary>
    string Name { get; }


    /// <summary>
    /// Initializes this library and defines words from this library.
    /// </summary>
    /// <param name="interpreter">An interpreter.</param>
    void Initialize(IInterpreter interpreter);
}
