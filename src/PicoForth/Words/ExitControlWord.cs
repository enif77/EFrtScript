/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth.Words;


internal class ExitControlWord : IWord
{
    public string Name => "Exit";
    

    public void Execute(IEvaluator evaluator)
    {
        throw new NotImplementedException(Name);
    }
}

/*
 
// ( - )
private int ExitAction()
{
    if (_interpreter.IsCompiling == false)
    {
        throw new Exception("EXIT outside a new word definition.");
    }

    // EXIT word doesn't have a runtime behavior.

    _interpreter.WordBeingDefined.AddWord(new ExitControlWord(_interpreter, _interpreter.WordBeingDefined));

    return 1;
} 
  
  
/// <summary>
/// A word that is exiting a word definition.
/// </summary>
public class ExitControlWord : AWordBase
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="interpreter">An IInterpreter instance, that is executing this word..</param>
    /// <param name="definitionWord">A word, in which this DOES is used.</param>
    public ExitControlWord(IInterpreter interpreter, NonPrimitiveWord definitionWord)
        : base(interpreter)
    {
        Name = "EXIT";
        IsControlWord = true;
        Action = () => 
        {
            // Do not execute remaining words from the currently running word.
            _definitionWord.BreakExecution();

            return 1;
        };

        _definitionWord = definitionWord;
    }

    private readonly NonPrimitiveWord _definitionWord;
}  
  
  
 */