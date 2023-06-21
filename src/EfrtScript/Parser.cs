/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System;
using System.Text;

using EFrtScript.IO;
using EFrtScript.Values;


internal class Parser
{
    private readonly ISourceReader _source;
    

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="source">A source reader.</param>
    public Parser(ISourceReader source)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
    }


    /// <summary>
    /// Parses a word.
    /// </summary>
    /// <returns>A word or null if no more chars left.</returns>    
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
            wordBuff.Append((char)c);

            c = _source.NextChar();
        }

        return wordBuff?.ToString();
    }

    /// <summary>
    /// Parses a double quotes terminated string.
    /// </summary>
    /// <returns>A string.</returns>
    public string ParseString()
    {
        return ParseTerminatedString('\"', true);
    }

    /// <summary>
    /// Parses a terminated string literal.
    /// </summary>
    /// <param name="terminator">A character, that terminates the parsed string literal.</param>
    /// <param name="allowSpecialChars">If special (escape) characters are supported in the parsed string literal.</param>
    /// <returns>A string.</returns>
    public string ParseTerminatedString(char terminator, bool allowSpecialChars = false)
    {
        var stringBuff = new StringBuilder();

        var c = _source.NextChar();  // Skip the white-space behind the string literal opening word (.", ...).
        var terminatorFound = false;
        while (c >= 0)
        {
            if (c == terminator)
            {
                c =_source.NextChar();  // Eat the terminator.
                terminatorFound = true;

                break;
            }

            if (allowSpecialChars && c == '\\')
            {
                stringBuff.Append(ParseStringSpecialChar());
                c = _source.CurrentChar;  // The CurrentChar contains the character following the escaped special char.

                continue;
            }

            stringBuff.Append((char)c);

            c = _source.NextChar();
        }

        if (terminatorFound == false)
        {
            throw new Exception($"'{terminator}' string terminator expected.");
        }

        if (c >= 0 && IsWhiteSpace(c) == false)
        {
            throw new Exception("The EOF or an white character after a string terminator expected.");
        }

        return stringBuff.ToString();
    }

    /// <summary>
    /// Parses a special (or escape) characters defined for string literals.
    /// When finishes, the CurrentChar contains the character behind the escaped character.
    /// </summary>
    /// <returns>A string containing the parsed special character.</returns>
    private string ParseStringSpecialChar()
    {
        _source.NextChar();  // eat '\'

        switch (_source.CurrentChar)
        {
            case 'a': _source.NextChar(); return "\a";      // \a BEL (alert, ASCII 7)
            case 'b': _source.NextChar(); return "\b";      // \b BS (backspace, ASCII 8)
            case 'e': _source.NextChar(); return "\u001B";  // \e ESC (escape, ASCII 27)
            case 'f': _source.NextChar(); return "\f";      // \f FF (form feed, ASCII 12)
            case 'l': _source.NextChar(); return "\n";      // \l LF (line feed, ASCII 10)
            case 'm': _source.NextChar(); return "\r\n";    // \m CR/LF pair (ASCII 13, 10)
            case 'n': _source.NextChar(); return "\n";      // newline (implementation dependent , e.g., CR/LF, CR, LF, LF/CR)
            case 'q':                                       // \q double-quote(ASCII 34)
            case '\"': _source.NextChar(); return "\"";     // \" double-quote (ASCII 34)
            case 'r': _source.NextChar(); return "\r";      // \r CR (carriage return, ASCII 13)
            case 't': _source.NextChar(); return "\t";      // \t HT (horizontal tab, ASCII 9)
            case 'v': _source.NextChar(); return "\v";      // \v VT (vertical tab, ASCII 11)
            case 'z':                                       // \z NUL (no character, ASCII 0)
            case '0': _source.NextChar(); return "\0";      // \0 NUL (no character, ASCII 0)
            case '\\': _source.NextChar(); return "\\";     // \\ backslash itself (ASCII 92)
            case '\'': _source.NextChar(); return "\'";

            case 'u':                                       // A sequence of 4 hex characters.
                {
                    var c = _source.NextChar();          // eat 'u'
                    var charCode = 0;
                    for (var i = 0; i < 4; i++)
                    {
                        charCode = (charCode * 16) + CharToDigit((char)c, 16);

                        c = _source.NextChar();
                    }

                    return string.Empty + (char)charCode;
                }

            case 'x':                                       // A hex sequence of 1 to 4 hex characters.
            case 'X':
                {
                    var c = _source.NextChar();          // eat 'x'
                    var charCode = 0;
                    for (var i = 0; i < 4; i++)
                    {
                        if (IsDigitInternal(c, 16))
                        {
                            charCode = (charCode * 16) + CharToDigit((char)c, 16);
                        }
                        else
                        {
                            if (i == 0) throw new Exception("A hex digit expected in a \\x string escape character.");

                            break;
                        }

                        c = _source.NextChar();
                    }

                    return string.Empty + (char)charCode;
                }

            default:
                throw new Exception($"An unexpected character with code {_source.CurrentChar} in a string escape definition found.");
        }
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
    /// <param name="radix">A number conversion radix. Can be from the 2 .. 36 range.</param>
    /// <param name="result">When this method returns, contains the signed integer or floating point value equivalent of the number contained in s.</param>
    /// <param name="allowLeadingWhite">Allows leading white-space characters. Ex. "   123.4" is returned as 123.4.</param>
    /// <param name="allowTrailingWhite">Allows trailing white-space characters. Ex. "123.4   " is returned as 123.4.</param>
    /// <param name="allowTrailingChars">Allows non numeric characters behind the parsed number. Ex. "123.4aaa" is returned as 132.4.</param>
    /// <returns>Returns rue if s was converted successfully; otherwise, false.</returns>
    public static bool TryParseNumber(string? s, int radix, out IValue result, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
    {
        try
        {
            result = (radix == 10)
                ? ParseNumber(s, allowLeadingWhite, allowTrailingWhite, allowTrailingChars)
                : ParseInteger(s, radix, allowLeadingWhite, allowTrailingWhite, allowTrailingChars);

            return true;
        }
        catch
        {
            result = new IntegerValue(0);

            return false;
        }
    }

    /// <summary>
    /// Parses a number from a string. 
    /// Decimal numbers are supported only and floating point numbers are returned as type Double.
    /// </summary>
    /// <param name="s">A string to parse.</param>
    /// <param name="allowLeadingWhite">If true, leading white chars are ignored.</param>
    /// <param name="allowTrailingWhite">If true, trailing white chars are ignored.</param>
    /// <param name="allowTrailingChars">If true, trailing chars are ignored.</param>
    public static IValue ParseNumber(string? s, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("A non-empty numeric string expected.", nameof(s));
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
            throw new Exception("At least one digit expected.");
        }

        // Parse the first digit sequence.
        while (IsDigit(sourceReader.CurrentChar))
        {
            var digit = sourceReader.CurrentChar - '0';
            if (isFloatingPoint)
            {
                floatingPointValue = floatingPointValue * 10.0 + digit;
            }
            else
            {
                try
                {
                    checked
                    {
                        integerValue = (integerValue * 10) + digit;
                    }
                }
                catch (OverflowException)
                {
                    // An integer number overflow converts it to a floating point value.
                    floatingPointValue = integerValue * 10.0 + digit;
                    isFloatingPoint = true;
                }
            }

            sourceReader.NextChar();
        }
        
        // A floating point number?
        var hasFloatingPointMarker = false;
        if (sourceReader.CurrentChar is 'D' or 'd')
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
                throw new Exception("The fractional part of a floating point number cannot follow the floating point marker. (123D. is not allowed.)");
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
                throw new Exception("A fractional part of a real number expected.");
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
        if (sourceReader.CurrentChar is 'e' or 'E')
        {
            // 123De is not allowed.
            if (hasFloatingPointMarker)
            {
                throw new Exception("The scale-factor part of a floating point number cannot follow the floating point marker. (123De is not allowed.)");
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
                throw new Exception("A scale factor of a floating point number expected.");
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
            throw new Exception("No leading chars after the floating point number allowed.");
        }

        // We eat all chars, its a number.
        return isFloatingPoint
            ? new FloatValue(floatingPointValue * sign)
            : new IntegerValue(integerValue * sign);
    }

    /// <summary>
    /// Parses a string into an integer number using the provided number conversion radix.
    /// </summary>
    /// <param name="s">A string to parse.</param>
    /// <param name="radix">A number conversion radix. Can be from the 2 .. 36 range.</param>
    /// <param name="allowLeadingWhite">Indicates whether leading white chars are allowed.</param>
    /// <param name="allowTrailingWhite">Indicates whether trailing white chars are allowed.</param>
    /// <param name="allowTrailingChars">Indicates whether trailing chars are allowed.</param>
    public static IValue ParseInteger(string? s, int radix, bool allowLeadingWhite = false, bool allowTrailingWhite = false, bool allowTrailingChars = false)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new ArgumentException("A non-empty numeric string expected.", nameof(s));
        }

        CheckRadix(radix);

        var integerValue = 0;
        
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
        if (IsDigitInternal(sourceReader.CurrentChar, radix) == false)
        {
            // No digit yet = badly formatted number.
            throw new Exception("At least one digit expected.");
        }

        // Parse the digit sequence.
        while (IsDigitInternal(sourceReader.CurrentChar, radix))
        {
            integerValue = (integerValue * radix) + CharToDigit((char)sourceReader.CurrentChar, radix);
            sourceReader.NextChar();
        }
        
        // Skip leading white chars.
        while (allowTrailingWhite && IsWhiteSpace(sourceReader.CurrentChar))
        {
            sourceReader.NextChar();
        }

        // We expect to eat all chars from a word while parsing a number.
        if (sourceReader.CurrentChar >= 0 && allowTrailingChars == false)
        {
            throw new Exception("No leading chars after the integer number allowed.");
        }

        // We eat all chars, its a number.
        return new IntegerValue(integerValue * sign);
    }

    /// <summary>
    /// Indicates whether the specified Unicode character is categorized as white space.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>true, if c is a white space, otherwise false.</returns>
    public static bool IsWhiteSpace(int c)
    {
        return char.IsWhiteSpace((char)c);
    }

    /// <summary>
    /// Checks, if a character is a binary digit.
    /// A decimal digit is one of: '0', '1'.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>true, if c is a binary digit, otherwise false.</returns>
    public static bool IsBinaryDigit(int c)
    {
        return IsDigitInternal(c, 2);
    }

    /// <summary>
    /// Checks, if a character is an octal digit.
    /// A decimal digit is one of: '0', '1', '2', '3', '4', '5', '6', '7'.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>true, if c is a octal digit, otherwise false.</returns>
    public static bool IsOctalDigit(int c)
    {
        return IsDigitInternal(c, 8);
    }

    /// <summary>
    /// Checks, if a character is a decimal digit.
    /// A decimal digit is one of: '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>true, if c is a decimal digit, otherwise false.</returns>
    public static bool IsDigit(int c)
    {
        return IsDigitInternal(c, 10);
    }

    /// <summary>
    ///  Checks, if a character is a hexadecimal digit.
    /// A decimal digit is one of: 
    ///   '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
    ///   'a', 'b', 'c', 'd', 'e', 'f',
    ///   'A', 'B', 'C', 'D', 'E', 'F'.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <returns>true, if c is a hexadecimal digit, otherwise false.</returns>
    public static bool IsHexDigit(int c)
    {
        return IsDigitInternal(c, 16);
    }

    /// <summary>
    /// Checks, if a character is a digit of a certain radix.
    /// </summary>
    /// <param name="c">A character.</param>
    /// <param name="radix">A number conversion radix. Can be from the 2 .. 36 range.</param>
    /// <returns>true, if c is a digit of a radix, otherwise false.</returns>
    public static bool IsDigit(int c, int radix)
    {
        CheckRadix(radix);

        return IsDigitInternal(c, radix);
    }

    
    private static void CheckRadix(int radix)
    {
        if (radix is < 2 or > 36)
        {
            throw new ArgumentOutOfRangeException(nameof(radix), $"The {radix} radix is out of the 2 .. 36 range.");
        }
    }


    private static int CharToDigit(char c, int radix)
    {
        if (c < '0')
        {
            throw new Exception($"Unsupported char '{c}' for radix {radix}.");
        }
        
        if (radix <= 10)
        {
            if (c < radix + '0')
            {
                // 0 .. (radix - 1)
                return c - '0';
            }

            throw new Exception($"Unsupported char '{c}' for radix {radix}.");
        }
        
        if (c <= '9')
        {
            // O .. 9
            return c - '0';
        }
        
        if (c >= 'a')
        {
            // a .. (radix - 10 - 1)
            if (c < radix - 10 + 'a')
            {
                return (c - 'a') + 10;
            }

            // Above letters.
            throw new Exception($"Unsupported char '{c}' for radix {radix}.");
        }

        if (c >= 'A' && c < radix - 10 + 'A')
        {
            // A .. (radix - 10 - 1)
            return (c - 'A') + 10;
        }
        
        throw new Exception($"Unsupported char '{c}' for radix {radix}.");
    }


    private static bool IsDigitInternal(int c, int radix)
    {
        if (c < '0')
        {
            return false;
        }
        
        if (radix <= 10)
        {
            return c < radix + '0';
        }
        
        if (c <= '9')
        {
            // 0 .. 9
            return true;
        }
        
        if (c >= 'a')
        {
            // a .. (radix - 10 - 1)
            return c < radix - 10 + 'a';    
        }

        // A .. (radix - 10 - 1)
        return c >= 'A' && c < radix - 10 + 'A';
    }
}
