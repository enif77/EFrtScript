/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DoWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DO", new DoWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new DoWord().IsImmediate);
    }
}
