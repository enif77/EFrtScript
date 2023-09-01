/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class EndNewWordCompilationWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal(";", new EndNewWordCompilationWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new EndNewWordCompilationWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.State.InputSourceStack.Push(
            new TestInputSource(
                new StringSourceReader("test")));

        new BeginNewWordCompilationWord().Execute(interpreter);
        
        Assert.Equal(1, new EndNewWordCompilationWord().Execute(interpreter));
    }
    
    [Fact]
    public void EndsNewWordCompilation()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        interpreter.State.InputSourceStack.Push(
            new TestInputSource(
                new StringSourceReader("test")));

        new BeginNewWordCompilationWord().Execute(interpreter);
        
        Assert.True(interpreter.IsCompiling);

        new EndNewWordCompilationWord().Execute(interpreter);
        
        Assert.False(interpreter.IsCompiling);
    }
}
