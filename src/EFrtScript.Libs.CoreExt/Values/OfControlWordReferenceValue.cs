/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Values;

using EFrtScript.Libs.CoreExt.Words;


/// <summary>
/// Holds a reference to the OF word.
/// </summary>
internal class OfControlWordReferenceValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer => ControlWord.ExecutionToken;
    public double Float => Integer;
    public string String => ControlWord.Name;
    public OfControlWord ControlWord { get; }


    public OfControlWordReferenceValue(OfControlWord controlWord)
    {
        ControlWord = controlWord ?? throw new ArgumentNullException(nameof(controlWord));
    }
}
