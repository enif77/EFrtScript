/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Values;
using EFrtScript.Libs.CoreExt.Words;


public class CaseWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("CASE", new CaseWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new CaseWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test 1");
        
        Assert.Equal(1, new CaseWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void PushesReferenceToCaseControlWordToReturnStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test CASE");
        
        Assert.True(interpreter.ReturnStackPeek() is CaseControlWordReferenceValue);
    }
    
    
    [Fact]
    public void AddsCaseControlWordToCompiledWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test CASE");
        
        Assert.True(interpreter.WordBeingDefined!.GetWord(interpreter.WordBeingDefined!.NextWordIndex - 1) is CaseControlWord);
    }
}
