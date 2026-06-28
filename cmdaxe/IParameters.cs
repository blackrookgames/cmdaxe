using System;
using System.Collections.Generic;
using System.Reflection;

namespace cmdaxe
{
    /// <summary>Represents a collection of command-line parameters</summary>
    public interface IParameters<T> : IEnumerable<T> where T: IParameter
    {
        #region abstract properties

        /// <summary>Number of command-line parameters in collection</summary>
        public int Count { get; }

        #endregion

        #region abstract methods

        /// <summary>Attempts to find the parameter with the specified name</summary>
        /// <param name="name">Parameter name</param>
        /// <param name="func">Found parameter</param>
        /// <returns>Whether or not successful</returns>
        public bool TryGet(string name, out T func);

        #endregion
    }
}