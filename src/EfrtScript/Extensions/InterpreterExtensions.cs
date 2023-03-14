/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using EFrtScript;
using EFrtScript.Values;
using EFrtScript.Words;


/// <summary>
/// Interpreter class extensions. 
/// </summary>
public static class InterpreterExtensions
{
    /// <summary>
    /// Adds a primitive word to the words list.
    /// </summary>
    /// <param name="wordName">A new word name.</param>
    /// <param name="action">An action, this word is doing.</param>
    public static void AddPrimitiveWord(this IInterpreter interpreter, string wordName, Func<IInterpreter, int> action)
    {
        if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
        if (action == null) throw new ArgumentNullException(nameof(action));

        interpreter.RegisterWord(new PrimitiveWord(wordName.ToUpperInvariant(), action));
    }
}