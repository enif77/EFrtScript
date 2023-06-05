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
    }
}
