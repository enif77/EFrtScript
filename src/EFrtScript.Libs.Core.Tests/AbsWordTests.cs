/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class AbsWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("ABS", new AbsWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new AbsWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new AbsWord().Execute(interpreter));
    }
    
    
    [Theory]
    [InlineData( 0, 0)]
    [InlineData( 1, 1)]
    [InlineData(-1, 1)]
    public void CalculationResultsMatchExpectedIntegerMathResults(int a, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        
        new AbsWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsIntegerValue());
        Assert.Equal(expected, result.Integer);
    }
    
    
    [Theory]
    [InlineData( "0", 0)]
    [InlineData( "1", 1)]
    [InlineData("-1", 1)]
    public void CalculationResultsMatchExpectedStringIntegerMathResults(string a, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        
        new AbsWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsIntegerValue());
        Assert.Equal(expected, result.Integer);
    }
    
    
    [Theory]
    [InlineData( 0.0, 0.0)]
    [InlineData( 1.0, 1.0)]
    [InlineData(-1.0, 1.0)]
    public void CalculationResultsMatchExpectedFloatingPointMathResults(double a, double expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
    
        new AbsWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsFloatingPointValue());
        Assert.Equal(expected, result.Float);
    }
    
    [Theory]
    [InlineData( "0.0", 0.0)]
    [InlineData( "1.0", 1.0)]
    [InlineData("-1.0", 1.0)]
    public void CalculationResultsMatchExpectedStringFloatingPointMathResults(string a, double expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
    
        new AbsWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsFloatingPointValue());
        Assert.Equal(expected, result.Float);
    }
}
