/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DropWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DROP", new DropWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DropWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new DropWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void TopOneStackItemIsDroppedForTwoStackItems()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        interpreter.StackPush(2);
        
        new DropWord().Execute(interpreter);
        
        Assert.Equal(1, interpreter.GetStackDepth());
        Assert.Equal(1, interpreter.StackPop().Integer);
    }


    [Fact]
    public void StackIsEmptyAfterTopOneStackItemIsDropped()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new DropWord().Execute(interpreter);
        
        Assert.True(interpreter.StackIsEmpty());
    }
}
