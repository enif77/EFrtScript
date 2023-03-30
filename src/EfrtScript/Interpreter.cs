/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using EFrtScript.IO;
using EFrtScript.Words;


public class Interpreter : IInterpreter
{
    public IInterpreterState State { get; }
    public IOutputWriter Output { get; }
    public IInputSource? CurrentInputSource =>
        (State.InputSourceStack.Count > 0) 
            ? State.InputSourceStack.Peek() 
            : null;


    public Interpreter(IOutputWriter outputWriter)
    {
        State = new InterpreterState();
        Output = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
        
        _words = new Dictionary<string, IWord>();
    }


    #region words

    private readonly IDictionary<string, IWord> _words;
    
    public INonPrimitiveWord? WordBeingDefined { get; private set; }


    public bool IsWordRegistered(string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && _words.ContainsKey(wordName);

    
    public IWord GetRegisteredWord(string wordName)
    {
        if (IsWordRegistered(wordName) == false)
        {
            Throw(-13, $"The '{wordName}' word is undefined.");
        }

        return _words[wordName];
    }

    
    public void RegisterWord(IWord word)
        => _words.Add(word.Name.ToUpperInvariant(), word);


    public void BeginNewWordCompilation(string wordName)
    {
        if (IsCompiling)
        {
            Throw(-29, "compiler nesting");
        }

        WordBeingDefined = new NonPrimitiveWord(wordName);
        InterpreterState = InterpreterStateCode.Compiling;
    }


    public void EndNewWordCompilation()
    {
        if (IsCompiling == false)
        {
            Throw(-14, "not in a new word compilation");
        }

        RegisterWord(WordBeingDefined ?? throw new InvalidOperationException(nameof(WordBeingDefined) + " is null."));

        WordBeingDefined = null;
        InterpreterState = InterpreterStateCode.Interpreting;
    }

    #endregion


    #region execution

    public InterpreterStateCode InterpreterState { get; private set; }
    public bool IsCompiling => InterpreterState == InterpreterStateCode.Compiling;
    public bool IsExecutionTerminated => InterpreterState == InterpreterStateCode.Breaking || InterpreterState == InterpreterStateCode.Terminating;

    public event EventHandler<InterpreterEventArgs>? ExecutingWord;
    public event EventHandler<InterpreterEventArgs>? WordExecuted;


    public void Interpret(string src)
    {
        State.InputSourceStack.Push(
            new InputSource(
                new StringSourceReader(src)));
        
        while (true)
        {
            var word = CurrentInputSource!.ReadWordFromSource();
            if (word == null)
            {
                break;
            }

            var wordName = word.ToUpperInvariant();

            if (IsCompiling)
            {
                CompileWord(wordName);
            }
            else
            {
                ExecuteWord(wordName);
            }
            
            if (IsExecutionTerminated)
            {
                break;
            }
        }
        
        // Execution broken. Return to interpreting mode.
        if (InterpreterState == InterpreterStateCode.Breaking)
        {
            InterpreterState = InterpreterStateCode.Interpreting;
            //State.SetStateValue(false);
        }
            
        // Restore the previous input source, if any.
        if (State.InputSourceStack.Count > 0)
        {
            State.InputSourceStack.Pop();
        }
    }


    public int ExecuteWord(IWord word)
    {
        if (word == null) throw new ArgumentNullException(nameof(word));

        return ExecuteWordInternal(word);
    }
    

    public void Reset()
    {
        State.Reset();
        InterpreterState = InterpreterStateCode.Interpreting;
    }
    
    
    public void Abort()
    {
        State.Stack.Clear();
        State.InputSourceStack.Clear();

        // TODO: Clear the heap?
        // TODO: Clear the exception stack?

        Quit();
    }


    public void Quit()
    {
        State.ReturnStack.Clear();
        InterpreterState = InterpreterStateCode.Breaking;
    }
    
    
    public void Throw(int exceptionCode, string? message = null)
    {
        ThrowInternal(exceptionCode, message);
    }
    
    
    public void TerminateExecution()
    {
        InterpreterState = InterpreterStateCode.Terminating;
    }


    private void CompileWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            var word = _words[wordName];
            if (word.IsImmediate)
            {
                ExecuteWordInternal(_words[wordName]);
            }
            else
            {
                WordBeingDefined!.AddWord(word);
            }

            return;
        }
        
        if (wordName == WordBeingDefined?.Name)
        {
            // Recursive call of the currently defined word.
            WordBeingDefined.AddWord(WordBeingDefined);
            
            return;
        }
        
        if (int.TryParse(wordName, out var val))
        {
            WordBeingDefined!.AddWord(new ConstantValueWord(val));

            return;
        }

        ThrowInternal(-13, $"The '{wordName}' word is undefined.", true);
    }


    private void ExecuteWord(string wordName)
    {
        if (IsWordRegistered(wordName))
        {
            ExecuteWordInternal(_words[wordName]);
            
            return;
        }

        if (int.TryParse(wordName, out var val))
        {
            // This will show the value in the output of the TRACE word.
            State.CurrentWord = new ConstantValueWord(val);
            ExecuteWord(State.CurrentWord);

            return;
        }

        ThrowInternal(-13, $"The '{wordName}' word is undefined.", true);
    }


    private int ExecuteWordInternal(IWord word)
    {
        State.CurrentWord = word;

        try
        {
            ExecutingWord?.Invoke(this, new InterpreterEventArgs(word));

            return State.CurrentWord.Execute(this);
        }
        catch (InterpreterException ex)
        {
            Throw(ex.ExceptionCode, ex.Message);
        }
        catch (ExecutionException)
        {
            ; // Here we can do nothing, because all exceptions were already handled.
        }
        catch (Exception ex)
        {
            Throw(-100, ex.Message);
        }
        finally
        {
            WordExecuted?.Invoke(this, new InterpreterEventArgs(word));
        }

        return 1;
    }


    private void ThrowInternal(int exceptionCode, string? message = null, bool doNotThrowAfterAbort = false)
    {
        if (exceptionCode == 0)
        {
            return;
        }

        if (State.ExceptionStack.IsEmpty)
        {
            switch (exceptionCode)
            {
                case -1: break;
                case -2: Output.WriteLine(message ?? "Execution aborted!"); break;
                default:
                    Output.WriteLine($"Execution aborted: [{exceptionCode}] {message ?? string.Empty}");
                    break;
            }

            Abort();

            // Used when an unknown word is executed.
            if (doNotThrowAfterAbort)
            {
                return;
            }

            // Throw an exception here, so we never return to the caller!
            throw new ExecutionException();
        }

        // Restore the previous execution state.
        var exceptionFrame = State.ExceptionStack.Pop();

        State.Stack.Top = exceptionFrame!.StackTop;
        State.ReturnStack.Top = exceptionFrame.ReturnStackTop;
        State.InputSourceStack.Top = exceptionFrame.InputSourceStackTop;
        State.CurrentWord = exceptionFrame.ExecutingWord ?? throw new InvalidOperationException("Exception frame without a executing word reference.");

        // Will be caught by the CATCH word.
        throw new InterpreterException(exceptionCode, message);
    }

    #endregion
}
