/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DupeWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DUP", new DupeWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DupeWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new DupeWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void TopStackValueIsDuplicated()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new DupeWord().Execute(interpreter);
        
        Assert.Equal(1, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
    }
}
