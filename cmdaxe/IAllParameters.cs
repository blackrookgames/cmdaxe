using System;
using System.Collections.Generic;
using System.Reflection;

namespace cmdaxe
{
    /// <summary>Represents all command-line parameters for a command</summary>
    public interface IAllParameters : IParameters<IParameter>
    {
        #region abstract properties

        /// <summary>Parameters that must be given arguments by the user</summary>
        public IParameters<IRequired> Required { get; }

        /// <summary>Optional command-line parameters</summary>
        public IParameters<IOption> Optional { get; }

        /// <summary>Shortcuts</summary>
        public IOptionsWithShortcut Shortcuts { get; }

        #endregion
    }
}