using System;

namespace cmdaxe
{
    /// <summary>Thrown when an error occurs during command execution</summary>
    /// <remarks>Initializer for <see cref="CommandException"/></remarks>
    /// <param name="cmd">Related command</param>
    /// <param name="message">Error message</param>
    public class CommandException(ICmdInfo? cmd, string? message) : Exception(message)
    {
        /// <summary>Initializer for <see cref="CommandException"/></summary>
        /// <param name="message">Error message</param>
        public CommandException(string? message) : this(null, message) { }

        #region fields
        
        private readonly ICmdInfo? f_Cmd = cmd;

        #endregion

        #region properties

        /// <summary>Related command</summary>
        public ICmdInfo? Cmd => f_Cmd;

        #endregion
    }
}