/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;


/// <summary>
/// Defines a branching/jumping to word.
/// </summary>
public interface IBranchingWord
{
    /// <summary>
    /// Sets a branch target index.
    /// </summary>
    /// <param name="branchIndex">A branch target index.</param>
    void SetBranchTargetIndex(int branchIndex);
}
