/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class IntWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("INT", new IntWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new IntWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new IntWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsIntegerForIntegerValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new IntWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(123, r.Integer);
    }
    
    [Fact]
    public void ReturnsIntegerForFloatingPointValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123.4);
        
        new IntWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(123, r.Integer);
    }
    
    [Fact]
    public void ReturnsIntegerForStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("123");
        
        new IntWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(123, r.Integer);
    }
}
