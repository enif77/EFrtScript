/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DupWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DUP", new DupWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DupWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new DupWord().Execute(interpreter));
    }
    
    [Fact]
    public void TopStackValueIsDuplicated()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new DupWord().Execute(interpreter);
        
        Assert.Equal(1, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
    }
}

/*

https://forth-standard.org/standard/core/DUP

Testing:

T{ 1 DUP -> 1 1 }T

 */