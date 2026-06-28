using System;

namespace cmdaxe
{
    /// <summary>
    ///     <para>
    ///         Specifies a command-line option that takes an argument
    ///     </para>
    ///     <para>
    ///         Arrays are also supported as an underlying field/property. 
    ///         However the keyword/shortcut must be used for every array item defined.
    ///         <br/>Example: --flags DEBUG --flags WINDOWS
    ///     </para>
    /// </summary>
    /// <param name="name">
    ///     Parameter name; this is also its keyword
    /// </param>
    /// <param name="shortcut">
    ///     Shortcut character
    /// </param>
    /// <param name="argType">
    ///     Argument display type; this tells the user what kind of argument should be inputted<br/>
    ///     Examples: "number", "name", "path", "directory"
    /// </param>
    /// <param name="desc">
    ///     Parameter description
    /// </param>
    public class OptionWArgAttribute(
        string name = null, char shortcut = '\0', string argType = null, string desc = null) : 
        OptionAttribute(name, shortcut, desc)
    {
        #region fields

        private readonly string f_ArgType = argType;

        #endregion

        #region properties

        /// <summary>
        ///     Argument display type; this tells the user what kind of argument should be inputted<br/>
        ///     Examples: "number", "name", "path", "directory"
        /// </summary>
        public string ArgType => f_ArgType;

        #endregion
    }
}