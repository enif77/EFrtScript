/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System.Text;

using EFrtScript.IO;
using EFrtScript.Values;


internal class Parser
{
    private readonly ISourceReader _source;
    

    public Parser(ISourceReader source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }


    public string? ParseWord()
    {
        StringBuilder? wordBuff = null;

        var c = _source.NextChar();
        while (c >= 0)
        {
            if (IsWhiteSpace(c))
            {
                if (wordBuff == null)
                {
                    c = _source.NextChar();

                    continue;
                }

                break;
            }

            wordBuff ??= new StringBuilder();
            wordBuff.Append((char) c);

            c = _source.NextChar();
        }

        return wordBuff?.ToString();
    }


    public string ParseString()
    {
        var stringBuff = new StringBuilder();
        var c = _source.NextChar();  // Skip the white-space behind the string literal opening word (S., .", ...).
        while (c >= 0)
        {
            if (c == '"')
            {
                break;
            }

            stringBuff.Append((char)c);

            c = _source.NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A string literal end expected");
        }

        return stringBuff.ToString();
    }


    /// <summary>
    /// Converts the string representation of a number to its signed integer or floating point equivalent. A return value indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="s">A string containing a number to convert.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <returns>Returns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(string? s, out IValue result)
    {
        return TryParseNumberEx(s, out result);
    }

    /// <summary>
    /// Parses a integer or a floating point number.
    /// It is called by the interpreter directly, because a word must be checked, if it is defined/known, 
    /// before it is parsed as a number.
    /// number :: [ sign ] ( unsigned-integer-number | unsigned-floating-point-number-with-marker | unsigned-floating-point-number ) .
    /// unsigned-integer-number :: digit-sequence .
    /// unsigned-floating-point-number-with-marker :: digit-sequence ( 'D' | 'd' ) .
    /// unsigned-floating-point-number :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
    /// scale-factor :: [ sign ] digit-sequence .
    /// fractional-part :: digit-sequence .
    /// sign :: '+' | '-' .
    /// </summary>
    /// <param name="s">A string containing a number to convert.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <returns>Returns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumberEx(string? s, out IValue result, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = new IntegerValue(0);

            return false;
        }

        var isFloatingPoint = false;
        var integerValue = 0;
        var floatingPointValue = 0.0;

        var sourceReader = new StringSourceReader(s);

        // Read the first char.
        sourceReader.NextChar();

        // Skip leading white chars.
        while (allowLeadingWhite && IsWhiteSpace(sourceReader.CurrentChar))
        {
            sourceReader.NextChar();
        }

        // Parse sign, if provided.
        var sign = 1;
        if (sourceReader.CurrentChar == '-')
        {
            sign = -1;
            sourceReader.NextChar();
        }
        else if (sourceReader.CurrentChar == '+')
        {
            sourceReader.NextChar();
        }

        // Check, that we have at least one digit here.
        if (IsDigit(sourceReader.CurrentChar) == false)
        {
            // No digit yet = badly formatted number.
            result = new IntegerValue(0);

            return false;
        }

        // Parse the first digit sequence.
        while (IsDigit(sourceReader.CurrentChar))
        {
            if (isFloatingPoint)
            {
                floatingPointValue = (floatingPointValue * 10.0) + (sourceReader.CurrentChar - '0');
            }
            else
            {
                var newIntegerValue = (integerValue * 10) + (sourceReader.CurrentChar - '0');
                
                if (newIntegerValue < 0)
                {
                    // An integer number overflow converts it to a floating point value.
                    floatingPointValue = (integerValue * 10.0) + (sourceReader.CurrentChar - '0');
                    isFloatingPoint = true;
                }
                else
                {
                    integerValue = newIntegerValue;
                }
            }

            sourceReader.NextChar();
        }


        result = new IntegerValue(666);
        return true;
        

        // A floating point number?
        var hasFloatingPointMarker = false;
        if (sourceReader.CurrentChar == 'D' || sourceReader.CurrentChar == 'd')
        {
            // D is ignored, if we are already in floating point mode.
            if (isFloatingPoint == false)
            {
                floatingPointValue = integerValue;
                isFloatingPoint = true;
            }

            // Eat 'd' or 'D'.
            sourceReader.NextChar();

            hasFloatingPointMarker = true;
        }

        // digit-sequence '.' fractional-part
        if (sourceReader.CurrentChar == '.')
        {
            // 123D. is not allowed.
            if (hasFloatingPointMarker)
            {
                result = new IntegerValue(0);

                return false;
            }

            if (isFloatingPoint == false)
            {
                floatingPointValue = integerValue;
                isFloatingPoint = true;
            }
            
            // Eat '.'.
            sourceReader.NextChar();

            if (IsDigit(sourceReader.CurrentChar) == false)
            {
                //throw new Exception("A fractional part of a real number expected.");
                result = new IntegerValue(0);

                return false;
            }

            var scale = 1.0;
            var frac = 0.0;
            while (IsDigit(sourceReader.CurrentChar))
            {
                frac = (frac * 10.0) + (sourceReader.CurrentChar - '0');
                scale *= 10.0;

                sourceReader.NextChar();
            }

            floatingPointValue += frac / scale;
        }

        // digit-sequence [ '.' fractional-part ] 'e' scale-factor
        if (sourceReader.CurrentChar == 'e' || sourceReader.CurrentChar == 'E')
        {
            // 123De is not allowed.
            if (hasFloatingPointMarker)
            {
                result = new IntegerValue(0);

                return false;
            }

            if (isFloatingPoint == false)
            {
                floatingPointValue = integerValue;
                isFloatingPoint = true;
            }

            // Eat 'e' or 'E'.
            sourceReader.NextChar();

            // scale-factor :: [ sign ] digit-sequence .
            var scaleFactorSign = 1.0;
            if (sourceReader.CurrentChar == '-')
            {
                scaleFactorSign = -1.0;
                sourceReader.NextChar();
            }
            else if (sourceReader.CurrentChar == '+')
            {
                sourceReader.NextChar();
            }
            
            if (IsDigit(sourceReader.CurrentChar) == false)
            {
                //throw new Exception("A scale factor of a real number expected.");
                result = new IntegerValue(0);

                return false;
            }

            var fact = 0.0;
            while (IsDigit(sourceReader.CurrentChar))
            {
                fact = (fact * 10.0) + (sourceReader.CurrentChar - '0');

                sourceReader.NextChar();
            }

            floatingPointValue *= Math.Pow(10, fact * scaleFactorSign);
        }

        // Skip leading white chars.
        while (allowTrailingWhite && IsWhiteSpace(sourceReader.CurrentChar))
        {
            sourceReader.NextChar();
        }

        // We expect to eat all chars from a word while parsing a number.
        if (sourceReader.CurrentChar >= 0 && allowTrailingChars == false)
        {
            result = new IntegerValue(0);

            return false;
        }

        // We eat all chars, its a number.
        result = isFloatingPoint
            ? new FloatValue(floatingPointValue * sign)
            : new IntegerValue(integerValue * sign);

        return true;
    }

    /// <summary>
    /// Checks, if a character is a white character.
    /// white-char :: SPACE | TAB | CR | LF .
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>True, if a character is a white character.</returns>
    public static bool IsWhiteSpace(int c)
    {
        return char.IsWhiteSpace((char)c);
    }

    /// <summary>
    /// Checks, if an character is a digit.
    /// digit :: '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>True, if a character is a digit.</returns>
    public static bool IsDigit(int c)
    {
        return c >= '0' && c <= '9';
    }

    /// <summary>
    ///  Defines hexadecimal-digit characters recognized by the language.
    /// 
    /// <pre>
    /// hexadecimal-digit ::
    ///   digit |
    ///   'a' | 'b' | 'c' | 'd' | 'e' | 'f' |
    ///   'A' | 'B' | 'C' | 'D' | 'E' | 'F' .
    /// </pre>
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>Returns true, if the given character is a hexadecimal-digit.</returns>
    public static bool IsHexDigit(int c)
    {
        return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
    }
}
