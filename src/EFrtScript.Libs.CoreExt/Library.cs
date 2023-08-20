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
    }
}
