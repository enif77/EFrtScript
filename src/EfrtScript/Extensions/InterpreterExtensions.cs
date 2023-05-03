/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using System.Text;

using EFrtScript;
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
    /// Throws "-14, interpreting a compile-only word", if the interpreter is not in the compilation mode.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="word">An optional word, that was executing.</param>
    public static void CheckIsCompiling(this IInterpreter interpreter, IWord word)
    {
        if (interpreter.IsCompiling == false)
        {
            interpreter.Throw(-14, $"interpreting a compile-only word {word.Name}");
        }
    }


    public static string ToStringValue(this IInterpreter interpreter, int value, int radix)
    {
        if (radix <= 1 || radix > 36)
        {
            // -24 invalid numeric argument
            throw new ArgumentOutOfRangeException(nameof(radix));
        }
            
        if (value == 0)
        {
            return "0";
        }
            
        var negative = value < 0;

        if (negative) 
        {
            value = -value;
        }
            
        var sb = new StringBuilder();

        for (; value > 0; value /= radix)
        {
            var d = value % radix;

            sb.Append((char)(d < 10 ? '0' + d : 'A' - 10 + d));
        }

        return (negative ? "-" : "") + string.Concat(sb.ToString().Reverse());
    }
}
