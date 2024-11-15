/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt;

using EFrtScript.Extensions;
using EFrtScript.Libs.CoreExt.Words;


public class Library : IWordsLibrary
{
    public string Name => "CORE-EXT";
    
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new LineCommentWord());

        interpreter.RegisterWord(new HexWord());
        
        interpreter.RegisterWord(new IntWord());
        interpreter.RegisterWord(new FloatWord());
        interpreter.RegisterWord(new StringWord());
        
        interpreter.RegisterWord(new QuestionIntWord());
        interpreter.RegisterWord(new QuestionFloatWord());
        interpreter.RegisterWord(new QuestionStringWord());

        interpreter.RegisterWord(new AgainWord());
        interpreter.RegisterWord(new QuestionDoWord());
        
        interpreter.RegisterWord(new PickWord());
        
        interpreter.RegisterWord(new CaseWord());
        interpreter.RegisterWord(new OfWord());
        interpreter.RegisterWord(new EndOfWord());
        interpreter.RegisterWord(new EndCaseWord());
        
        interpreter.RegisterWord(new DeferWord());
        interpreter.RegisterWord(new DeferFetchWord());
        interpreter.RegisterWord(new DeferStoreWord());
        interpreter.RegisterWord(new IsWord());
        interpreter.RegisterWord(new QuestionDeferWord());
    }
}
