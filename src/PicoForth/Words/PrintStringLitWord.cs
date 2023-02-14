/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class PrintStringLitWord : IWord
{
    public string Name => ".\"";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write(interpreter.ReadStringFromSource());

        return 1;
    }
}
