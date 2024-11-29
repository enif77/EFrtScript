/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class StringQuestionWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING?", new StringQuestionWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new StringQuestionWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        Assert.Equal(1, new StringQuestionWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsTrueForStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("123");
        
        new StringQuestionWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(-1, r.Integer);
    }
    
    [Fact]
    public void ReturnsFalseForNonStringValue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(123);
        
        new StringQuestionWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(0, r.Integer);
    }
}
