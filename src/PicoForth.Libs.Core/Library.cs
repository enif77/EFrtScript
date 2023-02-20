/* Copyright (C) Premysl Fara and Contributors */

using PicoForth.Libs.Core.Words;

namespace PicoForth.Libs.Core;


public class Library : IWordsLibrary
{
    public string Name => "CORE";
    
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new CommentWord());
        interpreter.RegisterWord(new LineCommentWord());

        interpreter.RegisterWord(new BeginNewWordCompilationWord());
        interpreter.RegisterWord(new EndNewWordCompilationWord());

        interpreter.RegisterWord(new StoreWord());
        interpreter.RegisterWord(new FetchWord());

        interpreter.RegisterWord(new ToReturnStackWord());
        interpreter.RegisterWord(new FromReturnStackWord());
        interpreter.RegisterWord(new FetchReturnStackWord());

        interpreter.RegisterWord(new ConditionalDuplicateWord());
        interpreter.RegisterWord(new DropWord());
        interpreter.RegisterWord(new DupWord());
        interpreter.RegisterWord(new SwapWord());
        interpreter.RegisterWord(new OverWord());
        interpreter.RegisterWord(new RotWord());
        interpreter.RegisterWord(new DepthWord());

        interpreter.RegisterWord(new DotWord());
        interpreter.RegisterWord(new CrWord());
        interpreter.RegisterWord(new SpaceWord());
        interpreter.RegisterWord(new SpacesWord());
        interpreter.RegisterWord(new TypeWord());

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
        interpreter.RegisterWord(new PrintStringWord());
        interpreter.RegisterWord(new StringLitWord());

        interpreter.RegisterWord(new IfWord());
        interpreter.RegisterWord(new ThenWord());
        interpreter.RegisterWord(new ElseWord());

        interpreter.RegisterWord(new DoWord());
        interpreter.RegisterWord(new LoopWord());
        
        interpreter.RegisterWord(new AbortWord());
        interpreter.RegisterWord(new AbortWithMessageWord());
        interpreter.RegisterWord(new CatchWord());
        interpreter.RegisterWord(new ThrowWord());
        
        interpreter.RegisterWord(new GetExecutionTokenWord());
    }
}
