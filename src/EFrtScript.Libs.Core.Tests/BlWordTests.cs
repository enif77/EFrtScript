/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class BlWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("BL", new BlWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new BlWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new BlWord().Execute(interpreter));
    }
    
    [Fact]
    public void AsciiCodeForSpacePushedToStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new BlWord().Execute(interpreter);
        
        Assert.Equal(' ', interpreter.StackPop().Integer);
    }
}
