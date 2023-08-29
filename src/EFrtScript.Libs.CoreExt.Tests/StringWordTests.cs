/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class StringWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING", new StringWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new StringWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new StringWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsStringValueForIntegerValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new StringWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsStringValue());
        Assert.Equal("123", r.String);
    }
    
    [Fact]
    public void ReturnsStringValueForFloatingPointValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123.4);
        
        new StringWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsStringValue());
        Assert.Equal("123.4", r.String);
    }
    
    [Fact]
    public void ReturnsStringValueForStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("123");
        
        new StringWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsStringValue());
        Assert.Equal("123", r.String);
    }
}
