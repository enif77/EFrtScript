/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Libs.String.Words;


public class StringLengthWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING-LENGTH", new StringLengthWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.False(new StringLengthWord().IsImmediate);
    }
}
