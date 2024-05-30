/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class PickWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("PICK", new PickWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new PickWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        interpreter.StackPush(0);
        
        Assert.Equal(1, new PickWord().Execute(interpreter));
    }
    
    [Theory]
    [InlineData(0, 5)] 
    [InlineData(1, 4)]
    [InlineData(2, 3)]
    [InlineData(3, 2)]
    [InlineData(4, 1)]
    public void PicksExpectedValue(int index, int expectedValue)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);
        interpreter.StackPush(3);
        interpreter.StackPush(4);
        interpreter.StackPush(5);
        
        interpreter.StackPush(index);
        
        new PickWord().Execute(interpreter);
        
        var r = interpreter.StackPop();
        
        Assert.True(r.IsIntegerValue());
        Assert.Equal(expectedValue, r.Integer);
    }
}
