/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class EqualToZeroWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("0=", new EqualToZeroWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new EqualToZeroWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new EqualToZeroWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void ReturnsTrueIfZero()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(0);
        
        new EqualToZeroWord().Execute(interpreter);
        
        Assert.True(interpreter.StackPop().Boolean);
    }
    
    
    [Fact]
    public void ReturnsFalseIfNonZero()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new EqualToZeroWord().Execute(interpreter);
        
        Assert.False(interpreter.StackPop().Boolean);
    }
}
