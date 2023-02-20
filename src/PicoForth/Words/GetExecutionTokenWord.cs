/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class GetExecutionTokenWord : IWord
{
    public string Name => "[']";
    public bool IsImmediate => true;


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsCompiling == false)
        {
            throw new Exception("['] outside a new word definition.");
        }

        var word = interpreter.ReadWordFromSource();
        if (string.IsNullOrEmpty(word))
        {
            throw new InterpreterException("A word name expected.");
        }

        interpreter.WordBeingDefined!
            .AddWord(new ConstantValueWord(
                interpreter.GetRegisteredWord(word.ToUpperInvariant())));
        
        return 1;
    }
}
