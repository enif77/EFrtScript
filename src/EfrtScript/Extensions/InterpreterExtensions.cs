/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using System.Text;

using EFrtScript;
using EFrtScript.Values;
using EFrtScript.Words;


/// <summary>
/// Interpreter class extensions. 
/// </summary>
public static class InterpreterExtensions
{
    private const int NumericConversionRadixHeapIndex = 1;
    

    /// <summary>
    /// Checks, if a word is already registered.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="wordName">A new word name.</param>
    /// <returns>True, if a word is already registered.</returns>
    public static bool IsWordRegistered(this IInterpreter interpreter, string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && interpreter.State.Words.ContainsKey(wordName);

    /// <summary>
    /// Gets a registered word. Throws an exception if no such word is registered.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="wordName">A requested word name.</param>
    /// <returns>A registered word instance.</returns>
    public static IWord GetRegisteredWord(this IInterpreter interpreter, string wordName)
    {
        if (interpreter.IsWordRegistered(wordName) == false)
        {
            interpreter.Throw(-13, $"The '{wordName}' word is undefined.");
        }

        return interpreter.State.Words[wordName];
    }

    /// <summary>
    /// Registers a new word. Throws an exception, if such word is already registered.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="word">A word to be registered.</param>
    public static void RegisterWord(this IInterpreter interpreter, IWord word)
        => interpreter.State.Words.Add(word.Name.ToUpperInvariant(), word);

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

    /// <summary>
    /// Checks, if the provided numeric conversion radix is correct.
    /// Throws "-24, numeric conversion radix out of range", if the radix is out of the 2 .. 36 range." if not.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="radix">A numeric conversion radix.</param>
    public static void CheckNumericConversionRadix(this IInterpreter interpreter, int radix)
    {
        if (radix is < 2 or > 36)
        {
            interpreter.Throw(-24, $"Numeric radix {radix} is out of the 2 .. 36 range.");
        }
    }
    
    /// <summary>
    /// Gets numeric conversion radix heap index.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>Returns numeric conversion radix heap index.</returns>
    public static int GetNumericConversionRadixHeapIndex(this IInterpreter interpreter)
        => NumericConversionRadixHeapIndex;

    /// <summary>
    /// Gets numeric conversion radix.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <returns>Returns numeric conversion radix.</returns>
    public static int GetNumericConversionRadix(this IInterpreter interpreter)
    {
        return interpreter.HeapFetch(NumericConversionRadixHeapIndex).Integer;
    }

    /// <summary>
    /// Sets numeric conversion radix.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="radix">A numeric conversion radix in range 2 .. 36.</param>
    public static void SetNumericConversionRadix(this IInterpreter interpreter, int radix)
    {
        CheckNumericConversionRadix(interpreter, radix);
        
        interpreter.HeapStore(NumericConversionRadixHeapIndex, new IntegerValue(radix));
    }
    
    /// <summary>
    /// Converts an integer value to a string.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="value">An integer value.</param>
    /// <param name="radix">An output numeric radix from the 2 .. 36 range.</param>
    /// <returns>A string representing the given value in a provided radix.</returns>
    public static string ToStringValue(this IInterpreter interpreter, int value, int radix)
    {
        CheckNumericConversionRadix(interpreter, radix);
            
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

        if (negative)
        {
            sb.Append('-');
        }

        return string.Concat(sb.ToString().Reverse());
    }
}
