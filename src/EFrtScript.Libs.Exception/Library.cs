/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception;

using EFrtScript.Extensions;
using EFrtScript.Libs.Exception.Words;


public class Library : IWordsLibrary
{
    public string Name => "EXCEPTION";
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new AbortWord());
        interpreter.RegisterWord(new AbortWithMessageWord());
        interpreter.RegisterWord(new CatchWord());
        interpreter.RegisterWord(new ThrowWord());
    }
}