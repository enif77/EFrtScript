/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Values;

namespace EFrtScript.Extensions;

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
    
    
    #region parsing
    
    /// <summary>
    /// Converts the string representation of a number to its signed integer or floating point equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="s">A string containing a number to convert.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <returns>tReturns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(this IInterpreter interpreter, string? s, out IValue result)
    {
        /*
         
            result – When this method returns, contains the 32-bit signed integer value equivalent of the number
            contained in s, if the conversion succeeded, or zero if the conversion failed. The conversion fails
            if the s parameter is null or Empty, is not of the correct format, or represents a number less than
            Int32.MinValue or greater than Int32.MaxValue. This parameter is passed uninitialized; any value
            originally supplied in result will be overwritten.
          
         */
        
        if (int.TryParse(s, out var val))
        {
            result = new IntegerValue(val);

            return true;
        }

        result = new IntegerValue(0);

        return false;
    }
    
    #endregion
    
}
