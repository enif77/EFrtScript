/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class DotWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal(".", new DotWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new DotWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new DotWord().Execute(interpreter));
    }
    
    [Fact]
    public void IntegerIsPrintedToOutput()
    {
        var output = new TestsOutputWriter();
        var interpreter = new Interpreter(output);
        
        interpreter.StackPush(1);

        new DotWord().Execute(interpreter);
        
        Assert.True(output.WriteCalled);
        Assert.Equal("1", output.Output);
    }
}
