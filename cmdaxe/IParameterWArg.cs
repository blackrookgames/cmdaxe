using System;

namespace cmdaxe
{
    /// <summary>Represents a command-line parameter that takes an argument</summary>
    public interface IParameterWArg
    {
        #region properties

        /// <summary>Whether or not the underlying field/property is an array</summary>
        public bool IsArray { get; }

        /// <summary>
        ///     <param>Parse function</param>
        ///     <param>If the underlying field/property is an array, this function is used to parse each item in the array</param>
        /// </summary>
        public IParseFuncInfo ParseFunc { get; }

        #endregion
    }
}