/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.Core.Words;


public class SuspendNewWordCompilationWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("[", new SuspendNewWordCompilationWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new SuspendNewWordCompilationWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);

        interpreter.Interpret(": test");
        
        Assert.Equal(1, new SuspendNewWordCompilationWord().Execute(interpreter));
    }
    
    [Fact]
    public void SuspendsNewWordCompilation()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);

        interpreter.Interpret(": test [ ");
        
        Assert.True(interpreter.IsCompilationSuspended);
    }
}
