/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;


public class LibraryTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CORE", new Library().Name);
    }
    
    
    [Theory]
    [InlineData("ABS")]
    [InlineData("BASE")]
    [InlineData("BEGIN")]
    [InlineData("BL")]
    [InlineData("CR")]
    [InlineData("DECIMAL")]
    [InlineData("DUP")]
    [InlineData("EMIT")]
    [InlineData("EXECUTE")]
    [InlineData("EXIT")]
    [InlineData("LEAVE")]
    [InlineData("-")]
    [InlineData("NEGATE")]
    [InlineData("+")]
    [InlineData("+LOOP")]
    [InlineData("(")]
    [InlineData("'")]
    [InlineData("[']")]
    [InlineData("REPEAT")]
    [InlineData("UNTIL")]
    [InlineData("WHILE")]
    public void HasWordRegistered(string wordName)
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var coreLib = new Library();
        coreLib.Initialize(interpreter);
        
        Assert.True(interpreter.IsWordRegistered(wordName));
    }
}
