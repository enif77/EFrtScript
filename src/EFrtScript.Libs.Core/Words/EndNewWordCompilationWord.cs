/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


internal class EndNewWordCompilationWord : IWord
{
    public string Name => ";";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.EndNewWordCompilation();

        return 1;
    }
}
