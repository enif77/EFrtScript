/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;

public class LibraryTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING", new Library().Name);
    }
    
    
    [Theory]
    [InlineData("?STRING-IS-EMPTY")]
    [InlineData("?STRING-STARTS-WITH")]
    [InlineData("STRING-LENGTH")]
    [InlineData("STRING-SUBSTRING")]
    [InlineData("STRING-TRIM")]
    public void HasWordRegistered(string wordName)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var lib = new Library();
        lib.Initialize(interpreter);
        
        Assert.True(interpreter.IsWordRegistered(wordName));
    }
}