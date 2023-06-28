/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;


public class LibraryTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("EXCEPTION", new Library().Name);
    }
    
    
    [Theory]
    [InlineData("ABORT")]
    [InlineData("ABORT\"")]
    [InlineData("CATCH")]
    [InlineData("THROW")]
    public void HasWordRegistered(string wordName)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var coreLib = new Library();
        coreLib.Initialize(interpreter);
        
        Assert.True(interpreter.IsWordRegistered(wordName));
    }
}
