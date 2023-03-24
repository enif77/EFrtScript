/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class CrWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CR", new CrWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new CrWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new CrWord().Execute(interpreter));
    }
    
    [Fact]
    public void NewLineToOutputAdded()
    {
        var output = new CrTestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        new CrWord().Execute(interpreter);
        
        Assert.True(output.WriteLineCalled);
    }
}
