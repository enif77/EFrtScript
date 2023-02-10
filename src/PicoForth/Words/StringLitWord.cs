/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class StringLitWord : IWord
{
    public string Name => "S\"";
    public bool IsImmediate => true;


    public void Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("S\" outside a new word definition.");
        }
        
        interpreter.WordBeingDefined!.AddWord(new ConstantValueWord(interpreter.ReadStringFromSource()));
    }
}
