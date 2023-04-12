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
    /// <returns>tReturns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(string? s, out IValue result)
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

    /// <summary>
    /// Parses a single or double cell integer or a floating point number.
    /// It is called by the interpreter directly, because a word must be checked, if it is defined/known, 
    /// before it is parsed as a number.
    /// unsigned-single-cell-integer :: digit-sequence .
    /// unsigned-double-cell-integer :: digit-sequence ( 'L' | 'l' ) .
    /// unsigned-floating-point-number :: digit-sequence ( 'D' | 'd' ) .
    /// unsigned-number :: unsigned-single-cell-integer | unsigned-double-cell-integer | unsigned-floating-point-number .
    /// unsigned-floating-point-number :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
    /// scale-factor :: [ sign ] digit-sequence .
    /// fractional-part :: digit-sequence .
    /// sign :: '+' | '-' .
    /// </summary>
    public static bool TryParseNumberEx(string? s, out IValue result, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = new IntegerValue(0);

            return false;
        }

        var sourceReader = new StringSourceReader(s);

        // Read the first char of the word.
        sourceReader.NextChar();

        var isFloatingPoint = false;
        //var isDoubleCellInteger = false;
        var integerValue = 0L;
        var floatingPointValue = 0.0;

        // Skip leading white chars.
        while (allowLeadingWhite && IsWhiteSpace(sourceReader.CurrentChar))
        {
            sourceReader.NextChar();
        }

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

        var haveDigit = false;
        while (IsDigit(sourceReader.CurrentChar))
        {
            haveDigit = true;
            integerValue = (integerValue * 10) + (sourceReader.CurrentChar - '0');

            // An integer number (long) overflow.
            if (integerValue < 0)
            {
                //throw new Exception("An integer constant overflow: " + word);
                result = new IntegerValue(0);

                return false;
            }

            sourceReader.NextChar();
        }

        // Check, that we have at least one digit here.
        if (haveDigit == false)
        {
            // No digit yet = badly formatted number.
            //return Token.CreateWordToken(word);
            result = new IntegerValue(0);

            return false;
        }

        // A double cell integer?
        if (sourceReader.CurrentChar == 'L' || sourceReader.CurrentChar == 'l')
        {
            //isDoubleCellInteger = true;
            sourceReader.NextChar();
        }

        // A floating point number?
        if (sourceReader.CurrentChar == 'D' || sourceReader.CurrentChar == 'd')
        {
            // if (isDoubleCellInteger)
            // {
            //     // LD = bad number suffix.
            //     return Token.CreateWordToken(word);
            // }

            floatingPointValue = integerValue;
            isFloatingPoint = true;
            sourceReader.NextChar();
        }

        // digit-sequence '.' fractional-part
        if (sourceReader.CurrentChar == '.')
        {
            // // 123L. is not allowed.
            // if (isDoubleCellInteger)
            // {
            //     //throw new Exception("Unexpected character in double cell constant: " + word);
            //     return Token.CreateWordToken(word);
            // }

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
                //return Token.CreateWordToken(word);
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
            // // 123Le is not allowed.
            // if (isDoubleCellInteger)
            // {
            //     //throw new Exception("Unexpected character in double cell constant: " + word);
            //     return Token.CreateWordToken(word);
            // }

            if (isFloatingPoint == false)
            {
                floatingPointValue = integerValue;
                isFloatingPoint = true;
            }

            // Eat 'e'.
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
                //return Token.CreateWordToken(word);
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
            //return Token.CreateWordToken(word);
            result = new IntegerValue(0);

            return false;
        }

        // We eat all chars, its a number.
        if (isFloatingPoint)
        {
            //return Token.CreateFloatingPointToken((float)(floatingPointValue * sign));
            result = new FloatValue(floatingPointValue * sign);

            return true;
        }
        // else if (isDoubleCellInteger)
        // {
        //     return Token.CreateDoubleCellIntegerToken(integerValue * sign);
        // }
        else
        {
            integerValue *= sign;

            if (integerValue < int.MinValue || integerValue > int.MaxValue)
            {
                // A double cell integer must be marked as such type with the L suffix.
                //return Token.CreateWordToken(word);
                result = new FloatValue(integerValue);

                return true;
            }

            // A single cell integer found.
            //return Token.CreateSingleCellIntegerToken((int)integerValue);
            result = new IntegerValue((int)integerValue);

            return true;
        }
    }


    // /// <summary>
    // /// Skips all white characters in the input stream.
    // /// </summary>
    // public void SkipWhite()
    // {
    //     while (IsWhiteSpace(CurrentChar))
    //     {
    //         NextChar();
    //     }
    // }

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
