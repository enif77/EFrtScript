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
        var result = Parser.TryParseNumber(src, out var value, allowLeadingWhite: true, allowTrailingWhite: true);
        
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
        var result = Parser.TryParseNumber(src, out var value, allowLeadingWhite: true, allowTrailingWhite: true);
        
        Assert.True(result);
        Assert.True(value.IsFloatingPointValue());
        Assert.Equal(expected, value.Float);
    }
}
