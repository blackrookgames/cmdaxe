using System;

namespace cmdaxe
{
    /// <summary>Thrown when an error occurs during command execution</summary>
    public class CommandException : Exception
    {
        /// <summary>Initializer for <see cref="CommandException"/></summary>
        /// <param name="message">Error message</param>
        public CommandException(string message) : base(message) { }
    }
}