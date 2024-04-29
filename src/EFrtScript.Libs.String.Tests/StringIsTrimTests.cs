/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Libs.String.Words;


public class StringTrimWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING-TRIM", new StringTrimWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.False(new StringTrimWord().IsImmediate);
    }
}
