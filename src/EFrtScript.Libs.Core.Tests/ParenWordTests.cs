/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.Libs.Core.Words;


public class ParenWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("(", new ParenWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new ParenWord().IsImmediate);
    }
    
    
    [Theory]
    [InlineData("( A comment)1234", 1234)]
    [InlineData(": pc1 ( A comment)1234 ; pc1", 1234)]
    public void CommentedOutSourceCharsAreSkipped(string src, int expected)
    {
        var interpreter = TestsHelper.CreateInterpreter();
        
        interpreter.Interpret(src);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }
}
