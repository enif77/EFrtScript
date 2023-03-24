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
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="wordName">A new word name.</param>
    /// <param name="action">An action, this word is doing.</param>
    public static void AddPrimitiveWord(this IInterpreter interpreter, string wordName, Func<IInterpreter, int> action)
    {
        if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
        if (action == null) throw new ArgumentNullException(nameof(action));

        interpreter.RegisterWord(new PrimitiveWord(wordName.ToUpperInvariant(), action));
    }

    /// <summary>
    /// Check, if the interpreter is currently in a new word compilation.
    /// Throws -14, interpreting a compile-only word, if the interpreter is not in the compilation mode.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    public static void CheckIsCompiling(this IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            interpreter.Throw(-14, "interpreting a compile-only word");
        }
    }
}