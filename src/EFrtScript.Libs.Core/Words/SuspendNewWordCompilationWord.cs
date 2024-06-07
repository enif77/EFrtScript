/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Core.Words;


internal class SuspendNewWordCompilationWord : IWord
{
    public string Name => "[";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.SuspendNewWordCompilation();

        return 1;        
    }
}
