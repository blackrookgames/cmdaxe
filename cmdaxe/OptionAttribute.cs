using System;

namespace cmdaxe
{
    /// <summary>Specifies an optional command-line parameter</summary>
    /// <param name="name">Parameter name; this is also its keyword</param>
    /// <param name="shortcut">Shortcut character</param>
    /// <param name="desc">Parameter description</param>
    public abstract class OptionAttribute(string name, char shortcut, string desc) : 
        ParameterAttribute(name, desc)
    {
        #region fields

        private readonly char f_Shortcut = shortcut;

        #endregion

        #region properties

        /// <summary>Shortcut character</summary>
        public char Shortcut => f_Shortcut;

        #endregion
    }
}