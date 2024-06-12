/* Copyright (C) Premysl Fara and Contributors */

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Values;

namespace EFrtScript.Libs.CoreExt.Tests;

using Xunit;

using EFrtScript.IO;
using EFrtScript.Libs.CoreExt.Words;


public class EndCaseWordTests
{
    [Fact]
    public void HasExpectedName()
    {
        Assert.Equal("ENDCASE", new EndCaseWord().Name);
    }
    
    
    [Fact]
    public void IsImmediate()
    {
        Assert.True(new EndCaseWord().IsImmediate);
    }
    
    
    [Fact]
    public void GoesToTheNextWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test CASE");
        
        Assert.Equal(1, new EndCaseWord().Execute(interpreter));
    }
    
    
    [Fact]
    public void ChecksCaseIndexOnReturnStackExists()
    {
        var interpreter = new Interpreter(new NullOutputWriter());

        new Core.Library().Initialize(interpreter);
        
        var thrown = false;
        
        interpreter.InterpreterExceptionThrown += (sender, args) =>
        {
            Assert.Equal(-22, args.Exception.ExceptionCode);
            Assert.Equal("ENDCASE without CASE.", args.Exception.Message);
            
            thrown = true;
        };
        
        interpreter.Interpret(": test");
        interpreter.ReturnStackPush(1);

        try
        {
            new EndCaseWord().Execute(interpreter);
        }
        catch (ExecutionException)
        {
            // ignored
        }
        
        Assert.True(thrown);
    }
    
    
    [Fact]
    public void DropsCaseControlWordReferenceFromReturnStack()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test CASE");
        
        Assert.True(interpreter.ReturnStackPeek() is CaseControlWordReferenceValue);
        
        new EndCaseWord().Execute(interpreter);
        
        Assert.True(interpreter.ReturnStackIsEmpty());
    }
    
    
    [Fact]
    public void AddsEndCaseControlWordToCompiledWord()
    {
        var interpreter = new Interpreter(new NullOutputWriter());
        
        new Core.Library().Initialize(interpreter);
        new CoreExt.Library().Initialize(interpreter);
        
        interpreter.Interpret(": test CASE");
        
        new EndCaseWord().Execute(interpreter);
        
        Assert.True(interpreter.WordBeingDefined!.GetWord(interpreter.WordBeingDefined!.NextWordIndex - 1) is EndCaseControlWord);
    }
}

/*
   - set ENDCASE index to all ENDOF words in R and remove them
 */