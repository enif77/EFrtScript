/* Copyright (C) Premysl Fara and Contributors */

namespace EFrtScript;

using System;


/// <summary>
/// An exception thrown by the Interpreter.Throw() method.
/// This stops further word execution.
/// </summary>
internal class ExecutionException : Exception
{
}
 