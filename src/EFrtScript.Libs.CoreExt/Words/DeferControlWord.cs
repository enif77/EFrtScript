/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript.Libs.CoreExt.Words;

using EFrtScript.Extensions;


internal class DeferControlWord : IWord, IDeferredWord
{
    public string Name => _wordName;
    public bool IsImmediate => false;
    public int ExecutionToken { get; set; }
    public int DeferredWordBodyExecutionToken => _executionToken;


    public DeferControlWord(string wordName)
    {
        if (string.IsNullOrWhiteSpace(wordName))
        {
            throw new ArgumentException("A word name expected.");
        }

        _wordName = wordName;
    }


    public int Execute(IInterpreter interpreter)
    {
        if (interpreter.IsWordRegistered(_executionToken) == false)
        {
            throw new InterpreterException(-13, $"The {_executionToken} execution token of the '{_wordName}' word not found.");
        }

        return interpreter.ExecuteWord(
            interpreter.GetRegisteredWord(_executionToken));
    }


    public void SetDeferredWordBodyExecutionToken(int executionToken)
    {
        _executionToken = executionToken;
    }


    private readonly string _wordName;
    private int _executionToken = -1;
}
