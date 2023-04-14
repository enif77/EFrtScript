/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Tests;

using Xunit;

using EFrtScript;
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
    [InlineData("")]
    [InlineData(" string")]
    [InlineData(" string ")]
    public void ParseStringThrowsExceptionWhenStringIsNotTerminated(string src)
    {
        var p = new Parser(new StringSourceReader(src));
        
        Assert.Throws<Exception>(() => p.ParseString());
    }
    
    // [Theory]
    // [InlineData("1", '1')]
    // [InlineData("abcd", 'a')]
    // public void CurrentChar_is_the_first_source_char_when_NextChar_is_first_called(string src, char expectedChar)
    // {
    //     var r = new StringSourceReader(src);
    //     r.NextChar();
    //     
    //     Assert.Equal(expectedChar, (char)r.CurrentChar);
    // }
    //
    // [Theory]
    // [InlineData("1", '1')]
    // [InlineData("abcd", 'a')]
    // public void NextChar_returns_the_first_source_char_when_NextChar_is_first_called(string src, char expectedChar)
    // {
    //     var r = new StringSourceReader(src);
    //     
    //     Assert.Equal(expectedChar, (char)r.NextChar());
    // }
    //
    // [Theory]
    // [InlineData("")]
    // [InlineData("1")]
    // [InlineData("abcd")]
    // public void NextChar_sets_CurrentChar_when_NextChar_is_called(string src)
    // {
    //     var r = new StringSourceReader(src);
    //
    //     var c = r.NextChar();
    //     
    //     Assert.Equal(c, r.CurrentChar);
    // }
}
