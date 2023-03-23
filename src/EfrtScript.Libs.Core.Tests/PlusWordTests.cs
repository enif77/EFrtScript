/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class PlusWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("+", new PlusWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new PlusWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);

        Assert.Equal(1, new PlusWord().Execute(interpreter));
    }
    

    [Theory]
    [InlineData( 0,  5,  5)]
    [InlineData( 5,  0,  5)]
    [InlineData( 0, -5, -5)]
    [InlineData(-5,  0, -5)]
    [InlineData( 1,  2,  3)]
    [InlineData( 1, -2, -1)]
    [InlineData(-1,  2,  1)]
    [InlineData(-1, -2, -3)]
    [InlineData(-1,  1,  0)]
    public void CalculationResultsMatchExpectedIntegerMathResults(int a, int b, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new PlusWord().Execute(interpreter);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }


    [Theory]
    [InlineData( 0.0,  5.0,  5.0)]
    [InlineData( 5.0,  0.0,  5.0)]
    [InlineData( 0.0, -5.0, -5.0)]
    [InlineData(-5.0,  0.0, -5.0)]
    [InlineData( 1.0,  2.0,  3.0)]
    [InlineData( 1.0, -2.0, -1.0)]
    [InlineData(-1.0,  2.0,  1.0)]
    [InlineData(-1.0, -2.0, -3.0)]
    [InlineData(-1.0,  1.0,  0.0)]
    public void CalculationResultsMatchExpectedFloatingPointMathResults(double a, double b, double expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new PlusWord().Execute(interpreter);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }


    [Fact]
    public void FirstIntIsConvertedToFloatingPointNumber()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(1.0);
        
        new PlusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsRealValue());
        Assert.Equal(2.0, result.Real);
    }


    [Fact]
    public void SecondIntIsConvertedToFloatingPointNumber()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1.0);
        interpreter.StackPush(1);
        
        new PlusWord().Execute(interpreter);
        
        var result = interpreter.StackPop();

        Assert.True(result.IsRealValue());
        Assert.Equal(2.0, result.Real);
    }

}
