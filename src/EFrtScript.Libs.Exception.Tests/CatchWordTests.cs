/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Tests;

using Xunit;

using EFrtScript.Libs.Exception.Words;


public class CatchWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CATCH", new CatchWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new CatchWord().IsImmediate);
    }
}
