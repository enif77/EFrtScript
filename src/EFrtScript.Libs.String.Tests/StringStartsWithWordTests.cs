/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String.Tests;

using Xunit;

using EFrtScript.Libs.String.Words;


public class StringStartsWithWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("?STRING-STARTS-WITH", new StringStartsWithWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.False(new StringStartsWithWord().IsImmediate);
    }
}
