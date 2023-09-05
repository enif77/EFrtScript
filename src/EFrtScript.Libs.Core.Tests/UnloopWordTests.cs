/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class UnloopWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("UNLOOP", new UnloopWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new UnloopWord().IsImmediate);
    }
}
