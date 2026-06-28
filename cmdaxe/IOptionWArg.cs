using System;

namespace cmdaxe
{
    /// <summary>Specifies a command-line option that takes an argument</summary>
    public interface IOptionWArg : IOption, IParameterWArg
    {
        #region properties

        /// <summary>
        ///     Argument display type; this tells the user what kind of argument should be inputted<br/>
        ///     Examples: "number", "name", "path", "directory"
        /// </summary>
        public string ArgType { get; }

        #endregion
    }
}