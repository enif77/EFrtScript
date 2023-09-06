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
    [InlineData(":")]
    [InlineData(";")]
    [InlineData(".")]
    [InlineData("0=")]
    [InlineData("=")]
    [InlineData("ABS")]
    [InlineData("BASE")]
    [InlineData("BEGIN")]
    [InlineData("BL")]
    [InlineData("CR")]
    [InlineData("DECIMAL")]
    [InlineData("DEPTH")]
    [InlineData("DO")]
    [InlineData("DROP")]
    [InlineData("DUP")]
    [InlineData("?DUP")]
    [InlineData("ELSE")]
    [InlineData("EMIT")]
    [InlineData("EVALUATE")]
    [InlineData("EXECUTE")]
    [InlineData("EXIT")]
    [InlineData("I")]
    [InlineData("J")]
    [InlineData("LEAVE")]
    [InlineData("-")]
    [InlineData("NEGATE")]
    [InlineData("+")]
    [InlineData("+LOOP")]
    [InlineData("(")]
    [InlineData("'")]
    [InlineData("[']")]
    [InlineData("REPEAT")]
    [InlineData("UNLOOP")]
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
