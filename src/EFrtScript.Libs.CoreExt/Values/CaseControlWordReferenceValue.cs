/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Values;

using EFrtScript.Libs.CoreExt.Words;

/// <summary>
/// Holds a reference to the CASE word.
/// </summary>
internal class CaseControlWordReferenceValue : IValue
{
    public bool Boolean => Integer != 0;
    public int Integer => ControlWord.ExecutionToken;
    public double Float => Integer;
    public string String => ControlWord.Name;
    public CaseControlWord ControlWord { get; }


    public CaseControlWordReferenceValue(CaseControlWord controlWord)
    {
        ControlWord = controlWord ?? throw new ArgumentNullException(nameof(controlWord));
    }
}
