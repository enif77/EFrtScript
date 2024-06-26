﻿/* Copyright (C) Premysl Fara and Contributors */

// #region unit-tests
//
// using System.Runtime.CompilerServices;
//
// [assembly:InternalsVisibleTo("EFrtScript.Libs.Core.Tests")]
//
// #endregion

namespace EFrtScript.Libs.Core;

using EFrtScript.Extensions;
using EFrtScript.Libs.Core.Words;


public class Library : IWordsLibrary
{
    public string Name => "CORE";
    
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new EvaluateWord());
        interpreter.RegisterWord(new ExecuteWord());
        
        interpreter.RegisterWord(new ParenWord());

        interpreter.RegisterWord(new BeginNewWordCompilationWord());
        interpreter.RegisterWord(new SuspendNewWordCompilationWord());
        interpreter.RegisterWord(new ResumeNewWordCompilationWord());
        interpreter.RegisterWord(new EndNewWordCompilationWord());
        interpreter.RegisterWord(new ExitWord());

        interpreter.RegisterWord(new BaseWord());
        interpreter.RegisterWord(new DecimalWord());

        interpreter.RegisterWord(new StoreWord());
        interpreter.RegisterWord(new FetchWord());

        interpreter.RegisterWord(new ToReturnStackWord());
        interpreter.RegisterWord(new FromReturnStackWord());
        interpreter.RegisterWord(new FetchReturnStackWord());

        interpreter.RegisterWord(new QuestionDupeWord());
        interpreter.RegisterWord(new DropWord());
        interpreter.RegisterWord(new DupeWord());
        interpreter.RegisterWord(new SwapWord());
        interpreter.RegisterWord(new OverWord());
        interpreter.RegisterWord(new RotWord());
        interpreter.RegisterWord(new DepthWord());

        interpreter.RegisterWord(new EmitWord());
        interpreter.RegisterWord(new DotWord());
        interpreter.RegisterWord(new CrWord());
        interpreter.RegisterWord(new SpaceWord());
        interpreter.RegisterWord(new SpacesWord());
        interpreter.RegisterWord(new TypeWord());
        interpreter.RegisterWord(new BlWord());

        interpreter.RegisterWord(new AbsWord());
        interpreter.RegisterWord(new NegateWord());
        interpreter.RegisterWord(new PlusWord());
        interpreter.RegisterWord(new MinusWord());
        interpreter.RegisterWord(new StarWord());
        interpreter.RegisterWord(new SlashWord());

        interpreter.RegisterWord(new EqualToZeroWord());
        interpreter.RegisterWord(new EqualWord());
        interpreter.RegisterWord(new GreaterThanWord());
        interpreter.RegisterWord(new LessThanWord());
        interpreter.RegisterWord(new LessThanZeroWord());
        
        interpreter.RegisterWord(new PrintStringLitWord());
        interpreter.RegisterWord(new StringLitWord());

        interpreter.RegisterWord(new IfWord());
        interpreter.RegisterWord(new ThenWord());
        interpreter.RegisterWord(new ElseWord());

        interpreter.RegisterWord(new GetInnerLoopIndexWord());
        interpreter.RegisterWord(new GetOuterLoopIndexWord());
        interpreter.RegisterWord(new BeginWord());
        interpreter.RegisterWord(new UntilWord());
        interpreter.RegisterWord(new DoWord());
        interpreter.RegisterWord(new LoopWord());
        interpreter.RegisterWord(new PlusLoopWord());
        interpreter.RegisterWord(new WhileWord());
        interpreter.RegisterWord(new RepeatWord());
        interpreter.RegisterWord(new LeaveWord());
        interpreter.RegisterWord(new UnloopWord());

        interpreter.RegisterWord(new TickWord());
        interpreter.RegisterWord(new GetExecutionTokenWord());
        
        interpreter.RegisterWord(new LiteralWord());
        interpreter.RegisterWord(new ConstantWord());
    }
}
