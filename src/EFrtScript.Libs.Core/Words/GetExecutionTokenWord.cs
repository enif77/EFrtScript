/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;

using EFrtScript.Extensions;
using EFrtScript.Words;


internal class GetExecutionTokenWord : IWord
{
    public string Name => "[']";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        var wordName = interpreter.CurrentInputSource!.ReadWord();
        if (string.IsNullOrEmpty(wordName))
        {
            throw new InterpreterException("A word name expected.");
        }

        var wordNameUpper = wordName.ToUpperInvariant();
        if (interpreter.IsWordRegistered(wordNameUpper) == false)
        {
            throw new InterpreterException($"The word '{wordName}' is not registered.");
        }

        interpreter.WordBeingDefined!
            .AddWord(new ConstantValueWord(interpreter.GetRegisteredWord(wordNameUpper).ExecutionToken));
        
        return 1;
    }
}
