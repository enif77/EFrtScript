/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class PrintStringWord : IWord
{
    public string Name => "S.";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write(interpreter.StackPop().String);
    }
}
