/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class FloatWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("FLOAT", new FloatWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new FloatWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new FloatWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsFloatingPointForIntegerValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new FloatWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsFloatingPointValue());
        Assert.Equal(123.0, r.Float);
    }
    
    [Fact]
    public void ReturnsFloatingPointForFloatingPointValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123.4);
        
        new FloatWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsFloatingPointValue());
        Assert.Equal(123.4, r.Float);
    }
    
    [Fact]
    public void ReturnsFloatingPointForStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("123.4");
        
        new FloatWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsFloatingPointValue());
        Assert.Equal(123.4, r.Float);
    }
}
