/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class ElseWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("ELSE", new ElseWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new ElseWord().IsImmediate);
    }
}
