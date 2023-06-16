/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System;


/// <summary>
/// Interpreter event args.
/// </summary>
public class InterpreterEventArgs : EventArgs
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="word">A word.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the word parameter is null.</exception>
    public InterpreterEventArgs(IWord word)
    {
        Word = word ?? throw new ArgumentNullException(nameof(word));
    }

    /// <summary>
    /// A word, that will be executed or was executed.
    /// </summary>
    public IWord Word { get; } 
}