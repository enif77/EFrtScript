/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Tools;

using EFrtScript.Extensions;
using EFrtScript.Libs.Tools.Words;


public class Library : IWordsLibrary
{
    public string Name => "TOOLS";
    
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new ByeWord());
    }
}
