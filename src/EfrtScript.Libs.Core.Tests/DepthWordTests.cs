/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DepthWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DEPTH", new DepthWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DepthWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new DepthWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void TwoIsReturnedForTwoStackItems()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(1);
        
        new DepthWord().Execute(interpreter);
        
        Assert.Equal(2, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
    }


    [Fact]
    public void OneIsReturnedForOneStackItems()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new DepthWord().Execute(interpreter);
        
        Assert.Equal(1, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
    }


    [Fact]
    public void ZeroIsReturnedForEmptyStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new DepthWord().Execute(interpreter);
        
        Assert.Equal(0, interpreter.StackPop().Integer);
    }
}
