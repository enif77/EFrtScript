/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class TypeWord : IWord
{
    public string Name => "TYPE";
    public bool IsImmediate => false;


    public void Execute(IInterpreter interpreter)
    {
        interpreter.OutputWriter.Write(interpreter.StackPop().String);
    }
}
