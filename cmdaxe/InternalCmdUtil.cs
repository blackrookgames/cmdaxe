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
        
        #region methods

        /// <summary>Checks whether or not the specified string indicates a help keyword</summary>
        /// <param name="s">String to check</param>
        /// <returns>Whether or not <paramref name="s"/> indicates a help keyword</returns>
        public static bool IsHelp(string s) => s == HELP_KEYWORD || s == HELP_SHORTCUT;

        /// <summary>
        /// Wrapper for executing a command<br/>
        /// If a <see cref="CommandException"/> is thrown, it is caught and an exit code of 1 is returned.<br/>
        /// Otherwise an exit code of 0 is returned.
        /// </summary>
        /// <param name="action">Action to execute</param>
        /// <returns>Exit code</returns>
        public static int Wrap(Action action)
        {
            if (action is null) return 0;
            try
            {
                action();
                return 0;
            }
            catch (TargetInvocationException e)
            {
                Exception ee = e;
                while (ee is TargetInvocationException)
                    ee = ee.InnerException;
                if (ee is not CommandException) throw;
                Console.Error.WriteLine($"ERROR:\n{ee.Message}");
                return 1;
            }
            catch (CommandException e)
            {
                Console.Error.WriteLine($"ERROR:\n{e.Message}");
                return 1;
            }
        }

        #endregion
    }
}