/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.Exception.Words;

using EFrtScript.Extensions;


internal class AbortWithMessageWord : IWord
{
    public string Name => "ABORT\"";
    public bool IsImmediate => true;
    public int ExecutionToken { get; set; }


    public int Execute(IInterpreter interpreter)
    {
        interpreter.CheckIsCompiling(this);

        interpreter.WordBeingDefined!
            .AddWord(new AbortWithMessageControlWord(interpreter.CurrentInputSource!.ReadString()));
        
        return 1;
    }
}
