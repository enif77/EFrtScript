/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DotWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal(".", new DotWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DotWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new DotWord().Execute(interpreter));
    }
    
    [Theory]
    [InlineData( 0, "0")]
    [InlineData( 123, "123")]
    [InlineData(-1, "-1")]
    [InlineData(+1, "1")]
    public void IntegerIsPrintedToOutput(int value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new DotWord().Execute(interpreter);
        
        Assert.True(output.WriteCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
    
    [Theory]
    [InlineData( 0.0, "0")]
    [InlineData( 0.5, "0.5")]
    [InlineData( 123.0, "123")]
    [InlineData(-1.0, "-1")]
    [InlineData(+1.0, "1")]
    public void FloatingPointIsPrintedToOutput(double value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new DotWord().Execute(interpreter);
        
        Assert.True(output.WriteCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
    
    [Theory]
    [InlineData( "0", "0")]
    [InlineData( "0.0", "0")]
    [InlineData( "0.5", "0.5")]
    [InlineData( " 123 ", "123")]
    [InlineData(" -1", "-1")]
    [InlineData("+1 ", "1")]
    public void StringIsPrintedToOutput(string value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new DotWord().Execute(interpreter);
        
        Assert.True(output.WriteCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
}
