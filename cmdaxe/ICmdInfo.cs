using System;

namespace cmdaxe
{
    /// <summary>Represents information about a command</summary>
    public interface ICmdInfo
    {
        #region abstract properties

        /// <summary>Group command belongs to</summary>
        public ICmdGroup Group { get; }

        /// <summary>Type</summary>
        public Type Type { get; }

        /// <summary>Command name</summary>
        public string Name { get; }

        /// <summary>Command description</summary>
        public string Desc { get; }
        
        /// <summary>Keyword for displaying help</summary>
        public string HelpKeyword { get; }

        /// <summary>Shortcut for displaying help</summary>
        public char HelpShort { get; }

        #endregion

        #region abstract methods

        /// <summary>Creates an instance of the command</summary>
        /// <returns>Created instance</returns>
        private protected BaseCommand MM_Instantiate();

        #endregion

        #region internal methods

        /// <summary>Executes based on context input</summary>
        /// <param name="context">Current context</param>
        /// <exception cref="CommandException">
        ///     An error occurred while executing
        /// </exception>
        internal void MM_Run(IContext context)
        {
            void run()
            {
                context ??= Group.Groups.Context;
                var command = MM_Instantiate();
                // Prepare for excecution
                command.MM_Prepare(this, context);
                if (command.PP_HelpRequest) return;
                // Execute!!!
                command.Main();
            }
            try
            {
                InternalCmdUtil.WrapCatchError(run);
            }
            catch (CommandException e) when (e.Cmd is null)
            {
                throw new CommandException(this, e.Message);
            }
        }

        #endregion

        #region methods

        /// <summary>Executes based on context input</summary>
        /// <param name="context">Current context</param>
        /// <returns>Exit code</returns>
        public int Run(IContext context) => InternalCmdUtil.Wrap(() => MM_Run(context));

        /// <summary>Executes based on context input</summary>
        /// <returns>Exit code</returns>
        public int Run() => Run(null);

        /// <summary>Creates a string of the full syntax of the command</summary>
        /// <param name="context">Current context</param>
        /// <returns>Full syntax of the command</returns>
        public string GetSyntax(IContext context)
        {
            context ??= Group.Groups.Context;
            return $"{context.EntryName} {context.MM_GetPrefix()}{Name} {BaseCommand.MM_GetSyntax(context, Type)}";
        }

        /// <summary>Creates a string of the full syntax of the command</summary>
        /// <returns>Full syntax of the command</returns>
        public string GetSyntax() => GetSyntax(null);

        /// <summary>Checks whether or not the specified string indicates a help keyword</summary>
        /// <param name="s">String to check</param>
        /// <returns>Whether or not <paramref name="s"/> indicates a help keyword</returns>
        public bool IsHelp(string s)
        {
            if (HelpKeyword is not null && HelpKeyword.Length > 0 && s == $"--{HelpKeyword}")
                return true;
            if (HelpShort != '\0' && s == $"-{HelpShort}")
                return true;
            return false;
        }

        #endregion
    }
}