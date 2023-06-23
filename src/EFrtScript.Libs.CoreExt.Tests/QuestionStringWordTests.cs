/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class QuestionStringWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("?STRING", new QuestionStringWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new QuestionStringWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new QuestionStringWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsTrueForStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("123");
        
        new QuestionStringWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(-1, r.Integer);
    }
    
    [Fact]
    public void ReturnsFalseForNonStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new QuestionStringWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(0, r.Integer);
    }
}
