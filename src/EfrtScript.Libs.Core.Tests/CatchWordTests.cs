/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Libs.Core.Words;


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
