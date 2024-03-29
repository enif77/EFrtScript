/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class MinusWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("-", new MinusWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new MinusWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);

        Assert.Equal(1, new MinusWord().Execute(interpreter));
    }
    
    
    [Theory]
    [InlineData( 0,  5, -5)]
    [InlineData( 5,  0,  5)]
    [InlineData( 0, -5,  5)]
    [InlineData(-5,  0, -5)]
    [InlineData( 1,  2, -1)]
    [InlineData( 1, -2,  3)]
    [InlineData(-1,  2, -3)]
    [InlineData(-1, -2,  1)]
    [InlineData( 0,  1, -1)]
    public void CalculationResultsMatchExpectedIntegerMathResults(int a, int b, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new MinusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsIntegerValue());
        Assert.Equal(expected, result.Integer);
    }
    
    
    [Theory]
    [InlineData( 0.0,  5.0, -5.0)]
    [InlineData( 5.0,  0.0,  5.0)]
    [InlineData( 0.0, -5.0,  5.0)]
    [InlineData(-5.0,  0.0, -5.0)]
    [InlineData( 1.0,  2.0, -1.0)]
    [InlineData( 1.0, -2.0,  3.0)]
    [InlineData(-1.0,  2.0, -3.0)]
    [InlineData(-1.0, -2.0,  1.0)]
    [InlineData( 0.0,  1.0, -1.0)]
    public void CalculationResultsMatchExpectedFloatingPointMathResults(double a, double b, double expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new MinusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsFloatingPointValue());
        Assert.Equal(expected, result.Float);
    }
    
    
    [Fact]
    public void FirstIntIsConvertedToFloatingPointNumber()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(3);
        interpreter.StackPush(1.0);
        
        new MinusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsFloatingPointValue());
        Assert.Equal(2.0, result.Float);
    }


    [Fact]
    public void SecondIntIsConvertedToFloatingPointNumber()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(3.0);
        interpreter.StackPush(1);
        
        new MinusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsFloatingPointValue());
        Assert.Equal(2.0, result.Float);
    }
}
