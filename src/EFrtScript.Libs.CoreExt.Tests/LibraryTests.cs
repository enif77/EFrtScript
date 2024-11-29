/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;


public class LibraryTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CORE-EXT", new Library().Name);
    }
    
    
    [Theory]
    [InlineData("AGAIN")]
    [InlineData(":NONAME")]
    [InlineData("CASE")]
    [InlineData("DEFER")]
    [InlineData("DEFER@")]
    [InlineData("DEFER!")]
    [InlineData("DEFER?")]
    [InlineData("ENDCASE")]
    [InlineData("ENDOF")]
    [InlineData("FLOAT")]
    [InlineData("FLOAT?")]
    [InlineData("HEX")]
    [InlineData("\\")]
    [InlineData("INT")]
    [InlineData("INT?")]
    [InlineData("IS")]
    [InlineData("OF")]
    [InlineData("PICK")]
    [InlineData("STRING")]
    [InlineData("STRING?")]
    [InlineData("?DO")]
    public void HasWordRegistered(string wordName)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        new Library().Initialize(interpreter);
        
        Assert.True(interpreter.IsWordRegistered(wordName));
    }
}
