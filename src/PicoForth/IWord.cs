/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IWord
{
    string Name { get; }
    bool IsImmediate { get; }

    void Execute(IEvaluator evaluator);
}
