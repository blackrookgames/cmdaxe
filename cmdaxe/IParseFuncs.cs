using System;
using System.Collections.Generic;

namespace cmdaxe
{
    /// <summary>Represents a collection of parse functions</summary>
    public interface IParseFuncs : IEnumerable<IParseFuncInfo>
    {
        #region abstract properties

        /// <summary>Number of parse functions in collection</summary>
        public int Count { get; }

        #endregion

        #region abstract methods

        /// <summary>Attempts to find the parse function with the specified target type</summary>
        /// <param name="type">Target type</param>
        /// <param name="func">Found function</param>
        /// <returns>Whether or not successful</returns>
        public bool TryGet(Type? type, out IParseFuncInfo? func);

        #endregion
    }
}