/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using System.Reflection;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class EmitWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("EMIT", new EmitWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new EmitWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new EmitWord().Execute(interpreter));
    }
    

    [Theory]
    [InlineData( 0,  "\0")]
    [InlineData( 32,  " ")]
    public void ExpectedCharAddedToOutput(int value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new EmitWord().Execute(interpreter);
        
        Assert.True(output.WriteCharCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
    
    [Theory]
    [InlineData( 0.0,  "\0")]
    [InlineData( 32.4,  " ")]
    public void ExpectedCharInFloatingPointAddedToOutput(double value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new EmitWord().Execute(interpreter);
        
        Assert.True(output.WriteCharCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
    
    [Theory]
    [InlineData( "0.0",  "\0")]
    [InlineData( "32.4",  " ")]
    [InlineData( " 32",  " ")]
    [InlineData( " 0 ",  "\0")]
    public void ExpectedCharInStringAddedToOutput(string value, string expectedOutput)
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(value);

        new EmitWord().Execute(interpreter);
        
        Assert.True(output.WriteCharCalled);
        Assert.Equal(expectedOutput, output.Output);
    }
}
