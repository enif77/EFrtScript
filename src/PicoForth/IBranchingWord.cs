/* Copyright (C) Premysl Fara and Contributors */

namespace PicoForth;


public interface IBranchingWord
{
    /// <summary>
    /// Sets a branch target index.
    /// </summary>
    /// <param name="branchIndex">A branch target index.</param>
    void SetBranchTargetIndex(int branchIndex);
}