using System;
using System.Collections.Generic;

namespace cmdaxe
{
    /// <summary>Represents a command group</summary>
    public interface ICmdGroup : IEnumerable<ICmdInfo>
    {
        #region abstract properties

        /// <summary>All command groups</summary>
        public ICmdGroups Groups { get; }

        /// <summary>Group name</summary>
        public string Name { get; }

        /// <summary>Number of commands in group</summary>
        public int Count { get; }

        #endregion

        #region abstract methods

        /// <summary>Attempts to find the command with the specified name</summary>
        /// <param name="name">Command name</param>
        /// <param name="command">Found command</param>
        /// <returns>Whether or not successful</returns>
        public bool TryGet(string name, out ICmdInfo command);

        #endregion

        #region helper methods

        /// <summary>Executes based on context input</summary>
        /// <param name="context">
        ///     Current context
        /// </param>
        /// <param name="noHelp">
        ///     If true, the first input item will not be checked to see if it is a help keyword
        /// </param>
        /// <exception cref="CommandException">
        ///     An error occurred while executing
        /// </exception>
        internal void MM_Run(IContext context, bool noHelp)
        {
            context ??= Groups.Context;
            // Has the user asked for help?
            bool helpMe = context.RawInput.Length == 0;
            if (!(noHelp || helpMe))
                helpMe = InternalCmdUtil.IsHelp(context.RawInput[0]);
            if (helpMe)
            {
                foreach (var _command in this)
                {
                    Console.WriteLine();
                    Console.WriteLine(_command.GetSyntax(context));
                    Console.WriteLine(_command.Desc);
                }
                Console.WriteLine();
                return;
            }
            // Find command
            var commandName = context.RawInput[0];
            if (!TryGet(commandName, out var command))
                throw new CommandException($"Invalid command: {context.MM_GetPrefix()}{commandName}");
            // Execute command
            var newContext = new InternalContext(
                (context.Original is null) ? context : context.Original,
                context.Prefixes,
                context.RawInput[1..]);
            command.MM_Run(newContext);
        }

        #endregion

        #region methods

        /// <summary>Executes a command based on context input</summary>
        /// <param name="context">Current context</param>
        /// <returns>Exit code</returns>
        public int Run(IContext context) => InternalCmdUtil.Wrap(() => MM_Run(context, false));

        /// <summary>Executes a command based on context input</summary>
        /// <returns>Exit code</returns>
        public int Run() => Run(null);

        #endregion
    }
}