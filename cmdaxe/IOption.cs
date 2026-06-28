using System;

namespace cmdaxe
{
    /// <summary>Represents an optional command-line parameter</summary>
    public interface IOption : IParameter
    {
        #region properties

        /// <summary>Shortcut character</summary>
        public char Shortcut { get; }

        #endregion
    }
}