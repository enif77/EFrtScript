/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Tests;

using Xunit;

using PicoForth.Extensions;
using PicoForth.IO;
using PicoForth.Libs.Core.Words;


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
    public void CalculationResultsMatchExpectedMathResults(int a, int b, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new MinusWord().Execute(interpreter);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }
}

/*

https://forth-standard.org/standard/core/Minus

Testing:

T{          0  5 - ->       -5 }T
T{          5  0 - ->        5 }T
T{          0 -5 - ->        5 }T
T{         -5  0 - ->       -5 }T
T{          1  2 - ->       -1 }T
T{          1 -2 - ->        3 }T
T{         -1  2 - ->       -3 }T
T{         -1 -2 - ->        1 }T
T{          0  1 - ->       -1 }T
T{ MID-UINT+1  1 - -> MID-UINT }T

 */