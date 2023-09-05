/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class EqualWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("=", new EqualWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new EqualWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);

        Assert.Equal(1, new EqualWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void ReturnsTrueIfEqual()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(10);
        interpreter.StackPush(10);
        
        new EqualWord().Execute(interpreter);
        
        Assert.True(interpreter.StackPop().Boolean);
    }
    
    
    [Fact]
    public void ReturnsFalseIfNotEqual()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);
        
        new EqualWord().Execute(interpreter);
        
        Assert.False(interpreter.StackPop().Boolean);
    }
}
