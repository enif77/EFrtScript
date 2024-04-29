/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Libs.String.Words;


public class StringIsEmptyWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING-IS-EMPTY", new StringIsEmptyWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.False(new StringIsEmptyWord().IsImmediate);
    }
}
