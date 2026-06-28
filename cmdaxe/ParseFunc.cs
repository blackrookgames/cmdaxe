using System;

namespace cmdaxe
{
    /// <summary>Attempts to parse the specified string input</summary>
    /// <param name="input">String input</param>
    /// <param name="result">Parse result</param>
    /// <returns>Whether or not successful</returns>
    public delegate bool ParseFunc(string input, out object result);
}