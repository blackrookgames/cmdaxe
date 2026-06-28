using System;

namespace cmdaxe
{
    /// <summary>Represents a command-line flag</summary>
    public interface IOptionFlag : IOption
    {
        #region properties

        /// <summary>Whether or not this is the help flag</summary>
        public bool IsHelp { get; }

        #endregion
    }
}