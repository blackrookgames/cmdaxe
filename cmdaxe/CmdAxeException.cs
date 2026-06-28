using System;

namespace cmdaxe
{
    /// <summary>Thrown when a <see cref="cmdaxe"/>-related error occurs</summary>
    public class CmdAxeException : Exception
    {
        /// <summary>Initializer for <see cref="CmdAxeException"/></summary>
        /// <param name="message">Error message</param>
        public CmdAxeException(string message) : base(message) { }

        /// <summary>Initializer for <see cref="CmdAxeException"/></summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner excpetion</param>
        public CmdAxeException(string message, Exception inner) : base(message, inner) { }
    }
}