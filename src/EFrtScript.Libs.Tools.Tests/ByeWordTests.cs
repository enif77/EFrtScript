/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Tools.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Tools.Words;


public class ByeWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("BYE", new ByeWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new ByeWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new ByeWord().Execute(interpreter));
    }
    
    [Fact]
    public void ExpectedInterpreterStateIsSet()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.False(interpreter.IsExecutionTerminated);
        
        new ByeWord().Execute(interpreter);
        
        Assert.True(interpreter.IsExecutionTerminated);
    }
}
