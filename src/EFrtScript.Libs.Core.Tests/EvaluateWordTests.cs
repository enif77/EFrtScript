/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class EvaluateWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("EVALUATE", new EvaluateWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new EvaluateWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("1");

        Assert.Equal(1, new EvaluateWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void EvaluatesAScriptOnTopOfTheStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush("10");
        
        new EvaluateWord().Execute(interpreter);
        
        Assert.Equal(10, interpreter.StackPop().Integer);
    }
}
