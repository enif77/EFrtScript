/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class QuestionDupeWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("?DUP", new QuestionDupeWord().Name);
    }
    
    
    [Fact]
    public void IsNotImmediate()
    {
        Assert.False(new QuestionDupeWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);

        Assert.Equal(1, new QuestionDupeWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void TopStackValueIsDuplicatedIfTrue()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.StackPush(1);
        
        new QuestionDupeWord().Execute(interpreter);
        
        Assert.Equal(2, interpreter.GetStackDepth());
        Assert.Equal(1, interpreter.StackPop().Integer);
        Assert.Equal(1, interpreter.StackPop().Integer);
    }
    
    
    [Fact]
    public void TopStackValueIsNotDuplicatedIfFalse()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        // An empty string is false and not integer value.
        interpreter.StackPush(string.Empty);
        
        new QuestionDupeWord().Execute(interpreter);
        
        Assert.Equal(1, interpreter.GetStackDepth());

        var result = interpreter.StackPop();
        
        // We expect 0 (Integer) on the stack instead of the original value.
        Assert.True(result.IsIntegerValue());
        Assert.Equal(0, result.Integer);
    }
}
