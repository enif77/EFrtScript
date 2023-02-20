/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Libs.Core.Words;

using PicoForth.Extensions;


/// <summary>
/// A word keeping a message and aborting program execution.
/// </summary>
internal class AbortWithMessageControlWord : IWord
{
    public string Name => "ABORT\"";
    public bool IsImmediate => false;
    
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message">A message.</param>
    public AbortWithMessageControlWord(string message)
    {
        _message = message;
    }
    

    public int Execute(IInterpreter interpreter)
    {
        //interpreter.StackExpect(1);

        if (interpreter.StackPop().Integer != 0)
        {
            interpreter.Throw(-2, _message);
        }
                
        return 1;
    }
    
    
    private readonly string _message;
}
