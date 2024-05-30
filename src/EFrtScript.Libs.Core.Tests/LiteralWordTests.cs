/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class LiteralWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("LITERAL", new LiteralWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new LiteralWord().IsImmediate);
    }
    
    [Fact]
    public void ExecuteAddsConstantWithExpectedValueToTheWordBeingDefined()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        var literalWord = new LiteralWord();
        
        interpreter.StackPush(42);
        
        interpreter.BeginNewWordCompilation("TEST");
        literalWord.Execute(interpreter);
        interpreter.EndNewWordCompilation();
        
        interpreter.Interpret("TEST");
        
        Assert.Equal(42, interpreter.StackPop().Integer);
    }
}
