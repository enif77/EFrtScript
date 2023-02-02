/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;

using System.Text;


internal class Tokenizer
{
    private ISourceReader _src;
    

    public Tokenizer(ISourceReader src)
    {
        _src = src ?? throw new ArgumentNullException(nameof(src));
    }


    public string? NextWord()
    {
        StringBuilder? wordBuff = null;

        var c = _src.NextChar();
        while (c >= 0)
        {
            if (char.IsWhiteSpace((char)c))
            {
                if (wordBuff == null)
                {
                    c = _src.NextChar();

                    continue;
                }

                break;
            }

            if (wordBuff == null)
            {
                wordBuff = new StringBuilder();
            }

            wordBuff.Append((char) c);

            c = _src.NextChar();
        }

        return wordBuff?.ToString();
    }
}
