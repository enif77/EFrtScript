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
    public void CalculationResultsMatchExpectedMathResults(int a, int b, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);
        interpreter.StackPush(b);
        
        new PlusWord().Execute(interpreter);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }
}

/*

https://forth-standard.org/standard/core/Plus

Testing:

T{        0  5 + ->          5 }T
T{        5  0 + ->          5 }T
T{        0 -5 + ->         -5 }T
T{       -5  0 + ->         -5 }T
T{        1  2 + ->          3 }T
T{        1 -2 + ->         -1 }T
T{       -1  2 + ->          1 }T
T{       -1 -2 + ->         -3 }T
T{       -1  1 + ->          0 }T
T{ MID-UINT  1 + -> MID-UINT+1 }T

 */