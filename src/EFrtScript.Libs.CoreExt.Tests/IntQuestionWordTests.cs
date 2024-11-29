/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class IntQuestionWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("INT?", new IntQuestionWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new IntQuestionWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new IntQuestionWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsTrueForIntegerValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new IntQuestionWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(-1, r.Integer);
    }
    
    [Fact]
    public void ReturnsFalseForNonIntegerValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123.5);
        
        new IntQuestionWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(0, r.Integer);
    }
}
