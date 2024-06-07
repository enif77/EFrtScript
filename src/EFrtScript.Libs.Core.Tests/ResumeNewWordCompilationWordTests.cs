/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class ResumeNewWordCompilationWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("]", new ResumeNewWordCompilationWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new ResumeNewWordCompilationWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);

        interpreter.Interpret(": test [ 1");
        
        Assert.Equal(1, new ResumeNewWordCompilationWord().Execute(interpreter));
    }
    
    [Fact]
    public void ResumesNewWordCompilation()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);

        interpreter.Interpret(": test [");
        
        Assert.True(interpreter.IsCompilationSuspended);
        
        interpreter.Interpret("1 ]");
        
        Assert.False(interpreter.IsCompilationSuspended);
        Assert.True(interpreter.IsCompiling);
    }
}
