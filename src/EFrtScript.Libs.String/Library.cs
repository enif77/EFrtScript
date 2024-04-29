/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.String;

using EFrtScript.Extensions;
using EFrtScript.Libs.String.Words;

public class Library : IWordsLibrary
{
    public string Name => "STRING";
    
    public void Initialize(IInterpreter interpreter)
    {
        interpreter.RegisterWord(new StringIsEmptyWord());
        interpreter.RegisterWord(new StringLengthWord());
        interpreter.RegisterWord(new StringTrimWord());
    }
}
