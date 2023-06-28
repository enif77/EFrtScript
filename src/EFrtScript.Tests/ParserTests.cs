/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Tests;

using Xunit;

using EFrtScript;
using EFrtScript.Extensions;
using EFrtScript.IO;


public sealed class ParserTests
{
    [Fact]
    public void ParseWordReturnsNullWhenNoMoreCharsAreInSource()
    {
        Assert.Null(new Parser(new StringSourceReader(string.Empty)).ParseWord());
    }

    [Theory]
    [InlineData("word", "word")]
    [InlineData(" word", "word")]
    [InlineData(" word ", "word")]
    [InlineData("123", "123")]
    [InlineData("a", "a")]
    public void ParseWordReturnsWholeWordWhenFound(string src, string expectedWord)
    {
        var p = new Parser(new StringSourceReader(src));
        
        Assert.Equal(expectedWord, p.ParseWord());
    }
    
    
    [Theory]
    [InlineData(".S string\"", "string")]
    [InlineData(".S  string\"", " string")]
    [InlineData(".S string with spaces\"", "string with spaces")]
    [InlineData(".S \"", "")]
    public void ParseStringReturnsExpectedString(string src, string expectedString)
    {
        var p = new Parser(new StringSourceReader(src));
        _ = p.ParseWord();
        
        Assert.Equal(expectedString, p.ParseString());
    }
    
    [Theory]
    [InlineData(".S")]
    [InlineData(".S ")]
    [InlineData(".S   ")]
    [InlineData(".S string")]
    [InlineData(".S string ")]
    public void ParseStringThrowsExceptionWhenStringIsNotTerminated(string src)
    {
        var p = new Parser(new StringSourceReader(src));
        _ = p.ParseWord();
        
        Assert.Throws<Exception>(() => p.ParseString());
    }
    
    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("-1", -1)]
    [InlineData("+1", 1)]
    [InlineData("123", 123)]
    [InlineData(" 0 ", 0)]
    [InlineData(" 1 ", 1)]
    [InlineData(" -1 ", -1)]
    [InlineData(" +1 ", 1)]
    [InlineData(" 123 ", 123)]
    public void TryParseNumberReturnsExpectedInteger(string src, int expected)
    {
        var result = Parser.TryParseNumber(src, 10, out var value, allowLeadingWhite: true, allowTrailingWhite: true);
        
        Assert.True(result);
        Assert.True(value.IsIntegerValue());
        Assert.Equal(expected, value.Integer);
    }

    [Theory]
    [InlineData("0.0", 0.0)]
    [InlineData("0d", 0.0)]
    [InlineData("1.0", 1.0)]
    [InlineData("1D", 1.0)]
    [InlineData("1e2", 100)]
    [InlineData("1.5", 1.5)]
    [InlineData("100e-2", 1.0)]
    [InlineData("100.465e+2", 10046.5)]
    public void TryParseNumberReturnsExpectedFloatingPoint(string src, double expected)
    {
        var result = Parser.TryParseNumber(src, 10, out var value, allowLeadingWhite: true, allowTrailingWhite: true);
        
        Assert.True(result);
        Assert.True(value.IsFloatingPointValue());
        Assert.Equal(expected, value.Float);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData('/', false)]
    [InlineData('0', true)]
    [InlineData('1', true)]
    [InlineData('2', false)]
    [InlineData('A', false)]
    [InlineData('a', false)]
    [InlineData('{', false)]
    public void IsDigitReturnsExpectedResultForBinaryDigits(char c, bool expected)
    {
        Assert.Equal(expected, Parser.IsDigit(c, 2));
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData('/', false)]
    [InlineData('0', true)]
    [InlineData('3', true)]
    [InlineData('7', true)]
    [InlineData('8', false)]
    [InlineData('A', false)]
    [InlineData('a', false)]
    [InlineData('{', false)]
    public void IsDigitReturnsExpectedResultForOctalDigits(char c, bool expected)
    {
        Assert.Equal(expected, Parser.IsDigit(c, 8));
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData('/', false)]
    [InlineData('0', true)]
    [InlineData('5', true)]
    [InlineData('9', true)]
    [InlineData(':', false)]
    [InlineData('A', false)]
    [InlineData('a', false)]
    [InlineData('{', false)]
    public void IsDigitReturnsExpectedResultForDecimalDigits(char c, bool expected)
    {
        Assert.Equal(expected, Parser.IsDigit(c, 10));
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData('/', false)]
    [InlineData('0', true)]
    [InlineData('5', true)]
    [InlineData('9', true)]
    [InlineData(':', false)]
    [InlineData('@', false)]
    [InlineData('A', true)]
    [InlineData('C', true)]
    [InlineData('F', true)]
    [InlineData('G', false)]
    [InlineData('`', false)]
    [InlineData('a', true)]
    [InlineData('c', true)]
    [InlineData('f', true)]
    [InlineData('g', false)]
    [InlineData('{', false)]
    public void IsDigitReturnsExpectedResultForHexadecimalDigits(char c, bool expected)
    {
        Assert.Equal(expected, Parser.IsDigit(c, 16));
    }

}
