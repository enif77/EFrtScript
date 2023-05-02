/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class HexWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("HEX", new HexWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new HexWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        Assert.Equal(1, new HexWord().Execute(interpreter));
    }
    
    [Fact]
    public void SetsExpectedNumberConversionRadix()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new HexWord().Execute(interpreter);
        
        var r = interpreter.HeapFetch(Library.NumbericConversionRadixHeapIndex);

        Assert.True(r.IsIntegerValue());
        Assert.Equal(16, r.Integer);
    }
}
