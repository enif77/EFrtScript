/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Libs.String.Words;


public class StringSubstringWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("STRING-SUBSTRING", new StringSubstringWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.False(new StringSubstringWord().IsImmediate);
    }
}
