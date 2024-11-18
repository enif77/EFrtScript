/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using EFrtScript.Extensions;
using EFrtScript.IO;
using EFrtScript.Words;


/// <summary>
/// Default interpreter implementation.
/// </summary>
public class Interpreter : IInterpreter
{
    /// <inheritdoc/>
    public IInterpreterState State { get; }
    
    /// <inheritdoc/>
    public IOutputWriter Output { get; }
    
    /// <inheritdoc/>
    public IInputSource? CurrentInputSource =>
        (State.InputSourceStack.Count > 0) 
            ? State.InputSourceStack.Peek() 
            : null;


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="outputWriter">An IOutputWriter instance used to output strings and chars from IO words.</param>
    /// <exception cref="ArgumentNullException">Thrown, when the outputWriter argument is null.</exception>
    public Interpreter(IOutputWriter outputWriter)
    {
        State = new InterpreterState();
        Output = outputWriter ?? throw new ArgumentNullException(nameof(outputWriter));
     
        InterpreterExceptionThrown += (sender, args) =>
        {
            Output.WriteLine($"Execution aborted: [{args.Exception.ExceptionCode}] {args.Exception.Message}");
        };
        
        this.SetNumericConversionRadix(10);
    }


    #region words compilation

    // A name of the anonymous word.
    private const string AnonymousWordName = ":NONAME WORD";
    
    
    /// <inheritdoc/>
    public INonPrimitiveWord? WordBeingDefined { get; private set; }

    
    /// <inheritdoc/>
    public void BeginNewWordCompilation(string wordName)
    {
        if (IsCompiling)
        {
            Throw(-29, "compiler nesting");
        }

        WordBeingDefined = new NonPrimitiveWord(wordName.ToUpperInvariant());
        _interpreterState = InterpreterStateCode.Compiling;
    }
    
    /// <inheritdoc/>
    public void BeginNewAnonymousWordCompilation()
    {
        // This word has no name. The registered name will be AnonymousWordName, which is not a valid word name.
        BeginNewWordCompilation(AnonymousWordName);
    }

    /// <inheritdoc/>
    public void SuspendNewWordCompilation()
    {
        // Cannot suspend a compilation, when not compiling.
        if (IsCompiling == false)
        {
            Throw(-14, "not in a new word compilation");
        }

        _interpreterState = InterpreterStateCode.SuspendingCompilation;
    }

    /// <inheritdoc/>
    public void ResumeNewWordCompilation()
    {
        // Cannot resume a compilation, when not suspended.
        if (_interpreterState != InterpreterStateCode.SuspendingCompilation)
        {
            Throw(-14, "new word compilation is not suspended");
        }

        _interpreterState = InterpreterStateCode.Compiling;
    }

    /// <inheritdoc/>
    public void EndNewWordCompilation()
    {
        if (IsCompiling == false)
        {
            Throw(-14, "not in a new word compilation");
        }

        if (WordBeingDefined == null)
        {
            throw new InvalidOperationException(nameof(WordBeingDefined) + " is null.");
        }
        
        // Registering the word will give us is execution token.
        this.RegisterWord(WordBeingDefined);

        // :NONAME, aka anonymous word, returns its execution token.
        if (WordBeingDefined.Name == AnonymousWordName)
        {
            this.StackPush(WordBeingDefined.ExecutionToken);
        }

        WordBeingDefined = null;
        _interpreterState = InterpreterStateCode.Interpreting;
    }

    #endregion


    #region execution

    private InterpreterStateCode _interpreterState;
    
    /// <inheritdoc/>
    public bool IsCompiling => _interpreterState == InterpreterStateCode.Compiling;
    
    /// <inheritdoc/>
    public bool IsCompilationSuspended => _interpreterState == InterpreterStateCode.SuspendingCompilation;
    
    /// <inheritdoc/>
    public bool IsExecutionTerminated => _interpreterState == InterpreterStateCode.Breaking || _interpreterState == InterpreterStateCode.Terminating;

    /// <inheritdoc/>
    public event EventHandler<InterpreterEventArgs>? ExecutingWord;
    
    /// <inheritdoc/>
    public event EventHandler<InterpreterEventArgs>? WordExecuted;

    /// <inheritdoc/>
    public event EventHandler<InterpreterExceptionEventArgs>? InterpreterExceptionThrown;
    

    /// <inheritdoc/>
    public void Interpret(string src)
    {
        State.InputSourceStack.Push(
            new InputSource(
                new StringSourceReader(src)));
        
        while (true)
        {
            var word = CurrentInputSource!.ReadWord();
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
        if (_interpreterState == InterpreterStateCode.Breaking)
        {
            _interpreterState = InterpreterStateCode.Interpreting;
            //State.SetStateValue(false);
        }
            
        // Restore the previous input source, if any.
        if (State.InputSourceStack.Count > 0)
        {
            State.InputSourceStack.Pop();
        }
    }

    /// <inheritdoc/>
    public int ExecuteWord(IWord word)
    {
        if (word == null) throw new ArgumentNullException(nameof(word));

        return ExecuteWordInternal(word);
    }
    
    /// <inheritdoc/>
    public void Reset()
    {
        State.Reset();
        _interpreterState = InterpreterStateCode.Interpreting;
    }
    
    /// <inheritdoc/>
    public void Abort()
    {
        State.Stack.Clear();
        State.InputSourceStack.Clear();

        // TODO: Clear the heap?
        // TODO: Clear the exception stack?

        Quit();
    }

    /// <inheritdoc/>
    public void Quit()
    {
        State.ReturnStack.Clear();
        _interpreterState = InterpreterStateCode.Breaking;
    }
    
    /// <inheritdoc/>
    public void Throw(int exceptionCode, string? message = null)
    {
        ThrowInternal(exceptionCode, message);
    }
    
    /// <inheritdoc/>
    public void TerminateExecution()
    {
        _interpreterState = InterpreterStateCode.Terminating;
    }


    private void CompileWord(string wordName)
    {
        if (this.IsWordRegistered(wordName))
        {
            var word = State.Words.Get(wordName);
            if (word.IsImmediate)
            {
                ExecuteWordInternal(word);
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
        
        if (Parser.TryParseNumber(wordName, this.GetNumericConversionRadix(), out var result))
        {
            WordBeingDefined!.AddWord(new ConstantValueWord(result));

            return;
        }

        ThrowInternal(-13, $"The '{wordName}' word is undefined.", true);
    }


    private void ExecuteWord(string wordName)
    {
        if (this.IsWordRegistered(wordName))
        {
            ExecuteWordInternal(State.Words.Get(wordName));
            
            return;
        }

        if (Parser.TryParseNumber(wordName, this.GetNumericConversionRadix(), out var result))
        {
            // This will show the value in the output of the TRACE word.
            ExecuteWordInternal(new ConstantValueWord(result));

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
            ThrowInternal(ex.ExceptionCode, ex.Message);
        }
        catch (ExecutionException)
        {
            // Here we can do nothing, because all exceptions were already handled.
        }
        catch (Exception ex)
        {
            ThrowInternal(-100, ex.Message);
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
                    InterpreterExceptionThrown?.Invoke(this, new InterpreterExceptionEventArgs(
                        new InterpreterException(exceptionCode, message)));
                    break;
            }

            Abort();

            // Used when an unknown word is executed.
            if (doNotThrowAfterAbort)
            {
                return;
            }

            // Throw an exception here, so we never return to the caller!
            throw new ExecutionException();  // TODO: Add the exception code and message to the ExecutionException.
        }

        // Create an interpreter exception.
        var interpreterException = new InterpreterException(exceptionCode, message);
        
        // Fire the event.
        InterpreterExceptionThrown?.Invoke(this, new InterpreterExceptionEventArgs(interpreterException));
        
        // Will be caught by the CATCH word.
        throw interpreterException;
    }

    #endregion
}
