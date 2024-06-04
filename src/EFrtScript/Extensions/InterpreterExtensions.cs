/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Extensions;

using System.Globalization;
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
    /// <param name="wordName">A word name.</param>
    /// <returns>True, if a word is already registered.</returns>
    public static bool IsWordRegistered(this IInterpreter interpreter, string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && interpreter.State.Words.IsDefined(wordName);

    /// <summary>
    /// Checks, if a word is already registered.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="executionToken">A word execution token.</param>
    /// <returns>True, if a word is already registered.</returns>
    public static bool IsWordRegistered(this IInterpreter interpreter, int executionToken)
        => interpreter.State.Words.IsDefined(executionToken);

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

        return interpreter.State.Words.Get(wordName);
    }

    /// <summary>
    /// Gets a registered word. Throws an exception if no such word is registered.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="executionToken">A requested word execution token.</param>
    /// <returns>A registered word instance.</returns>
    public static IWord GetRegisteredWord(this IInterpreter interpreter, int executionToken)
    {
        if (interpreter.IsWordRegistered(executionToken) == false)
        {
            interpreter.Throw(-13, $"A word with execution token {executionToken} is not defined.");
        }

        return interpreter.State.Words.Get(executionToken);
    }

    /// <summary>
    /// Registers a new word.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="word">A word to be registered.</param>
    public static void RegisterWord(this IInterpreter interpreter, IWord word)
        => interpreter.State.Words.Add(word);

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
    /// Adds an immediate primitive word to the words list.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="wordName">A new word name.</param>
    /// <param name="action">An action, this word is doing.</param>
    public static void AddImmediatePrimitiveWord(this IInterpreter interpreter, string wordName, Func<IInterpreter, int> action)
    {
        if (string.IsNullOrEmpty(wordName)) throw new ArgumentException("A word name expected.");
        if (action == null) throw new ArgumentNullException(nameof(action));

        interpreter.RegisterWord(new ImmediatePrimitiveWord(wordName.ToUpperInvariant(), action));
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
    
    #region Value conversions
    
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

    /// <summary>
    /// Try to parse a string as a number.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="s">A string containing a number to convert.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <returns>Returns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(this IInterpreter interpreter, string s, out IValue result)
    {
        return Parser.TryParseNumber(
            s,
            interpreter.GetNumericConversionRadix(),
            out result,
            allowLeadingWhite: true,
            allowTrailingWhite: true);
    }
    
    /// <summary>
    /// Converts a value to a string.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="value">An IValue instance to be converted to a string.</param>
    /// <returns>An IValue converted to string value.</returns>
    public static IValue ConvertToString(this IInterpreter interpreter, IValue value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (value.IsStringValue())
        {
            return value;
        }

        if (value.IsIntegerValue())
        {
            return new StringValue(interpreter.ToStringValue(value.Integer, interpreter.GetNumericConversionRadix()));
        }
        
        return new StringValue(value.IsFloatingPointValue()
                ? string.Format(CultureInfo.InvariantCulture, "{0}", value.Float)
                : value.String);
    }
    
    /// <summary>
    /// Converts a value to an integer.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="value">An IValue instance to be converted to an integer.</param>
    /// <returns>An IValue converted to integer value.</returns>
    public static IValue ConvertToInteger(this IInterpreter interpreter, IValue value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        if (value.IsStringValue())
        {
            if (Parser.TryParseNumber(
                    value.String,
                    interpreter.GetNumericConversionRadix(),
                    out var result,
                    allowLeadingWhite: true,
                    allowTrailingWhite: true))
            {
                // NOTE: We can return a floating point value here, if the string contains a floating point number.
                return result;
            }
            
            interpreter.Throw(-22, $"Invalid numeric string '{value.String}'");
        }

        // All non-string values can convert themself to an integer.
        return value;
    }

    /// <summary>
    /// Converts a value to a floating point number.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance.</param>
    /// <param name="value">An IValue instance to be converted to an integer.</param>
    /// <returns>An IValue converted to floating point value.</returns>
    public static IValue ConvertToFloat(this IInterpreter interpreter, IValue value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        var numericConversionRadix = interpreter.GetNumericConversionRadix();
        if (numericConversionRadix != 10)
        {
            interpreter.Throw(-40, "invalid BASE for floating point conversion");
        }
        
        if (value.IsStringValue())
        {
            if (Parser.TryParseNumber(
                    value.String,
                    numericConversionRadix,
                    out var result,
                    allowLeadingWhite: true,
                    allowTrailingWhite: true))
            {
                // NOTE: We can return an integer value here, if the value is integer.
                return result;
            }
            
            interpreter.Throw(-22, $"Invalid numeric string '{value.String}'");
        }

        // All non-string values can convert themself to a floating point value.
        return value;
    }

    #endregion
}
