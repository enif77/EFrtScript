/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class QuestionFloatWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("?FLOAT", new QuestionFloatWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new QuestionFloatWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new QuestionFloatWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsTrueForFloatingPointValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123.5);
        
        new QuestionFloatWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(-1, r.Integer);
    }
    
    [Fact]
    public void ReturnsFalseForNonFloatingPointValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new QuestionFloatWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(0, r.Integer);
    }
}
