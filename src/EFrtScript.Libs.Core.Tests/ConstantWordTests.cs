/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Extensions;

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class ConstantWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CONSTANT", new ConstantWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new ConstantWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
     
        interpreter.State.InputSourceStack.Push(
            new TestInputSource(
                new StringSourceReader("constant-name")));
        
        interpreter.StackPush(1);
        
        Assert.Equal(1, new ConstantWord().Execute(interpreter));
    }
}
