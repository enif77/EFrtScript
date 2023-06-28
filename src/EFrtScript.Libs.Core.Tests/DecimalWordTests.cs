/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DecimalWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("DECIMAL", new DecimalWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DecimalWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new DecimalWord().Execute(interpreter));
    }
    
    [Fact]
    public void SetsExpectedNumberConversionRadix()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new DecimalWord().Execute(interpreter);
        
        var r = interpreter.HeapFetch(1);

        Assert.True(r.IsIntegerValue());
        Assert.Equal(10, r.Integer);
    }
}
