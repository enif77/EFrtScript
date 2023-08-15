/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class BeginWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("BEGIN", new BeginWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new BeginWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.BeginNewWordCompilation("test");

        Assert.Equal(1, new BeginWord().Execute(interpreter));
    }
    
    // [Fact]
    // public void IsCompiledOnlyWord()
    // {
    //     var interpreter = new Interpreter(new NullOutputWriter());
        
    //     Assert.Throws<Exception>(() => new BeginWord().Execute(interpreter));
    // }

    [Fact]
    public void NextWordIndexPushedToStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());

        interpreter.BeginNewWordCompilation("test");

        new BeginWord().Execute(interpreter);
        
        Assert.Equal(0, interpreter.ReturnStackPop().Integer);
    }
}
