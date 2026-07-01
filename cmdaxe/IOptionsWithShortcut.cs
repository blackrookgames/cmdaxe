using System;
using System.Collections.Generic;
using System.Reflection;

namespace cmdaxe
{
    /// <summary>Represents a collection of optional command-line parameters with a shortcut</summary>
    public interface IOptionsWithShortcut : IEnumerable<IOption>
    {
        #region abstract properties

        /// <summary>Number of command-line parameters in collection</summary>
        public int Count { get; }

        #endregion

        #region abstract methods

        /// <summary>Attempts to find the parameter with the specified shortcut</summary>
        /// <param name="shortcut">Parameter shortcut</param>
        /// <param name="func">Found parameter</param>
        /// <returns>Whether or not successful</returns>
        public bool TryGet(char shortcut, out IOption? func);

        #endregion
    }
}