/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using System.Text;

using PicoForth.Values;
using PicoForth.Words;


public class Evaluator : IEvaluator
{
    private const int DefaultStackSize = 10;
    private const int DefaultReturnStackSize = 10;
    private const int DefaultHeapSize = 64;


    public Evaluator()
    {
        _source = new StringSourceReader(string.Empty);
        _words = new Dictionary<string, IWord>();
        _stack = new Stack<IValue>(DefaultStackSize);
        _returnStack = new Stack<IValue>(DefaultReturnStackSize);
        _heap = new IValue[DefaultHeapSize];

        RegisterBuildInWords();
    }


    public void Eval(string src)
    {
        _source = new StringSourceReader(src);

        var tokenizer = new Tokenizer(_source);
        
        while (true)
        {
            var word = tokenizer.NextWord();
            if (word == null)
            {
                break;
            }

            var wordName = word.ToUpperInvariant();
            if (IsWordRegistered(wordName))
            {
                _words[wordName].Execute(this);

                continue;
            }

            if (int.TryParse(word, out var val))
            {
                StackPush(new IntValue(val));

                continue;
            }
            
            throw new Exception($"Unknown word: {word}");    
        }
    }


    #region source

    private ISourceReader _source;


    public int CurrentChar => _source.CurrentChar;


    public int NextChar()
    {
        return _source.NextChar();
    }


    public string ReadStringFromSource()
    {
        var sb = new StringBuilder();
        var c = NextChar();  // Skip the white-space behind the string literal opening word (S., .", ...).
        while (c >= 0)
        {
            if (c == '"')
            {
                break;
            }

            sb.Append((char)c);

            c = NextChar();
        }

        if (c < 0)
        {
            throw new Exception("A string literal end expected");
        }

        return sb.ToString();
    }

    #endregion


    #region words

    private readonly IDictionary<string, IWord> _words;


    public bool IsWordRegistered(string wordName)
        => string.IsNullOrWhiteSpace(wordName) == false && _words.ContainsKey(wordName);


    public void RegisterWord(IWord word)
        => _words.Add(word.Name.ToUpperInvariant(), word);


    private void RegisterBuildInWords()
    {
        RegisterWord(new CommentWord());

        RegisterWord(new StoreWord());
        RegisterWord(new FetchWord());

        RegisterWord(new ToReturnStackWord());
        RegisterWord(new FromReturnStackWord());
        RegisterWord(new FetchReturnStackWord());

        RegisterWord(new ConditionalDuplicateWord());
        RegisterWord(new DropWord());
        RegisterWord(new DupWord());
        RegisterWord(new SwapWord());
        RegisterWord(new OverWord());
        RegisterWord(new RotWord());
        RegisterWord(new DepthWord());

        RegisterWord(new DotWord());
        RegisterWord(new CrWord());

        RegisterWord(new PlusWord());
        RegisterWord(new MinusWord());
        RegisterWord(new StarWord());
        RegisterWord(new SlashWord());

        RegisterWord(new EqualToZeroWord());
        RegisterWord(new EqualWord());
        RegisterWord(new GreaterThanWord());
        RegisterWord(new LessThanWord());
        RegisterWord(new LessThanZeroWord());

        RegisterWord(new PrintStringLitWord());
        RegisterWord(new PrintStringWord());
        RegisterWord(new ReadStringLitWord());
    }

    #endregion


    #region stack

    private Stack<IValue> _stack;


    public int StackDepth => _stack.Count;


    public void StackClear()
        => _stack = new Stack<IValue>(DefaultStackSize);

    public bool StackIsEmpty()
        => _stack.Count == 0;

    public IValue StackPeek()
        => _stack.Peek();

    public void StackPush(IValue v)
        => _stack.Push(v);

    public IValue StackPop()
        => _stack.Pop();

    #endregion


    #region return stack

    private Stack<IValue> _returnStack;


    public int ReturnStackDepth => _returnStack.Count;


    public void ReturnStackClear()
        => _returnStack = new Stack<IValue>(DefaultReturnStackSize);

    public bool ReturnStackIsEmpty()
        => _returnStack.Count == 0;

    public IValue ReturnStackPeek()
        => _returnStack.Peek();

    public void ReturnStackPush(IValue v)
        => _returnStack.Push(v);

    public IValue ReturnStackPop()
        => _returnStack.Pop();

    #endregion


    #region heap

    private readonly IValue[] _heap;


    public void HeapStore(int address, IValue value)
        => _heap[address] = value;


    public IValue HeapFetch(int address)
        => _heap[address];

    #endregion
}
