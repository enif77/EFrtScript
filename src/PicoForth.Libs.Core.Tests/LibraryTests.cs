/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Tests;

using Xunit;

using PicoForth.IO;


public class LibraryTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CORE", new Library().Name);
    }
    
    
    [Theory]
    [InlineData("DUP")]
    [InlineData("-")]
    [InlineData("NEGATE")]
    [InlineData("+")]
    public void HasWordRegistered(string wordName)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var coreLib = new Library();
        coreLib.Initialize(interpreter);
        
        Assert.True(interpreter.IsWordRegistered(wordName));
    }
}
