/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class BaseWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("BASE", new BaseWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new BaseWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new BaseWord().Execute(interpreter));
    }
    
    [Fact]
    public void ReturnsExpectedHeapIndex()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new BaseWord().Execute(interpreter);
        
        var r = interpreter.StackPop();

        Assert.True(r.IsIntegerValue());
        Assert.Equal(1, r.Integer);
    }
}
