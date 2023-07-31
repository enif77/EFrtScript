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

        var word = interpreter.CurrentInputSource!.ReadWord();
        if (string.IsNullOrEmpty(word))
        {
            throw new InterpreterException("A word name expected.");
        }

        interpreter.WordBeingDefined!
            .AddWord(new ConstantValueWord(word.ToUpperInvariant()));
        
        return 1;
    }
}
