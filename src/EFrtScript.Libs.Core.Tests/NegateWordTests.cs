/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class NegateWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("NEGATE", new NegateWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new NegateWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new NegateWord().Execute(interpreter));
    }
    
    [Theory]
    [InlineData( 0,  0)]
    [InlineData( 1, -1)]
    [InlineData(-1,  1)]
    [InlineData( 2, -2)]
    [InlineData(-2,  2)]
    public void CalculationResultsMatchExpectedMathResults(int a, int expected)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(a);

        new NegateWord().Execute(interpreter);
        
        Assert.Equal(expected, interpreter.StackPop().Integer);
    }
}

/*

https://forth-standard.org/standard/core/NEGATE

Testing:

T{  0 NEGATE ->  0 }T
T{  1 NEGATE -> -1 }T
T{ -1 NEGATE ->  1 }T
T{  2 NEGATE -> -2 }T
T{ -2 NEGATE ->  2 }T

 */