/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class BeginNewWordCompilationWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal(":", new BeginNewWordCompilationWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new BeginNewWordCompilationWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());

        interpreter.State.InputSourceStack.Push(
            new TestInputSource(
                new StringSourceReader("test")));
        
        Assert.Equal(1, new BeginNewWordCompilationWord().Execute(interpreter));
    }
    
    [Fact]
    public void StartsNewWordCompilation()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.State.InputSourceStack.Push(
            new TestInputSource(
                new StringSourceReader("test")));

        new BeginNewWordCompilationWord().Execute(interpreter);
        
        Assert.True(interpreter.IsCompiling);
    }
}
