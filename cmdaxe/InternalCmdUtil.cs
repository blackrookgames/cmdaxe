using System;
using System.Collections.Immutable;
using System.Reflection;

namespace cmdaxe
{
    internal static class InternalCmdUtil
    {
        #region const
        
        /// <summary>Help keyword</summary>
        public const string HELP_KEYWORD = "--help";
        
        /// <summary>Help shortcut keyword</summary>
        public const string HELP_SHORTCUT = "-h";

        #endregion
        
        #region helper methods

        /// <summary>
        ///     Assume
        ///     <br/>- <paramref name="error"/> is not null
        /// </summary>
        private static int MM_Error(CommandException error)
        {
            // Print message
            Console.Error.WriteLine($"ERROR:\n{error.Message}");
            // Print help info
            if (error.Cmd is not null)
            {
                var hasKeyword = !string.IsNullOrEmpty(error.Cmd.HelpKeyword);
                var hasShort = error.Cmd.HelpShort != '\0';
                if (hasKeyword || hasShort)
                {
                    Console.Error.WriteLine();
                    Console.Error.Write("Use ");
                    if (hasKeyword)
                    {
                        Console.Error.Write($"--{error.Cmd.HelpKeyword}");
                        if (hasShort) Console.Error.Write(" or ");
                    }
                    if (hasShort)
                    {
                        Console.Error.Write($"-{error.Cmd.HelpShort}");
                    }
                    Console.Error.WriteLine(" for help");
                }
            }
            // Return error code
            return 1;
        }

        #endregion
        
        #region methods

        /// <summary>Checks whether or not the specified string indicates a help keyword</summary>
        /// <param name="s">String to check</param>
        /// <returns>Whether or not <paramref name="s"/> indicates a help keyword</returns>
        public static bool IsHelp(string s) => s == HELP_KEYWORD || s == HELP_SHORTCUT;

        /// <summary>
        ///     Wrapper for executing a command<br/>
        ///     This catches any exceptions containing an inner <see cref="CommandException"/> and 
        ///     the inner <see cref="CommandException"/> is rethrown.
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <exception cref="CommandException">An error occurred</exception>
        public static void WrapCatchError(Action action)
        {
            if (action is null) return;
            try
            {
                action();
            }
            catch (CommandException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                while (e is not null)
                {
                    if (e is CommandException)
                        throw e;
                    e = e.InnerException;
                }
                throw;
            }
        }

        /// <summary>
        ///     Wrapper for executing a command<br/>
        ///     If a <see cref="CommandException"/> is thrown, it is caught and an exit code of 1 is returned.<br/>
        ///     Otherwise an exit code of 0 is returned.
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <returns>Exit code</returns>
        public static int Wrap(Action action)
        {
            if (action is null) return 0;
            try
            { action(); return 0; }
            catch (CommandException error)
            { return MM_Error(error); }
        }

        #endregion
    }
}