/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Values;
using EFrtScript.Libs.CoreExt.Words;


public class OfWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("OF", new OfWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new OfWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test 1 CASE 1");
        
        Assert.Equal(1, new OfWord().Execute(interpreter));
    }
}
