using System;
using System.Reflection;

namespace cmdaxe
{
    public abstract class BaseCommand
    {
        #region nested

        private delegate string GetSyntax(IContext context, Type type);

        #endregion

        #region internal methods

        /// <summary>Gets the input syntax of a command of the specified type</summary>
        /// <param name="context">Current context</param>
        /// <param name="type">Command type</param>
        /// <returns>Command input syntax</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null
        /// </exception>
        internal static string MM_GetSyntax(IContext context, Type type)
        {
            ArgumentNullException.ThrowIfNull(context);
            var testType = type;
            while (testType is not null)
            {
                // Search for method
                var method = testType.GetMethod(nameof(MM__GetSyntax),
                    BindingFlags.Static |
                    BindingFlags.NonPublic |
                    BindingFlags.Public);
                if (method is null) goto next;
                // Make sure method is static
                if (!method.IsStatic) goto next;
                // Make sure signature is correct
                GetSyntax getSyntax;
                try { getSyntax = method.CreateDelegate<GetSyntax>(); }
                catch { goto next; }
                // Call method
                return (string)method.Invoke(null, [context, type]);
                // Next
                next: testType = testType.BaseType;
            }
            return null;
        }

        #endregion

        #region abstract properties

        /// <summary>If true, the command should not execute as the help information was displayed instead.</summary>
        /// <remarks>This value should be updated during <see cref="MM_Prepare"/></remarks>
        protected internal abstract bool PP_HelpRequest { get; }

        #endregion

        #region abstract methods

        /// <summary>Gets the input syntax of the command</summary>
        /// <param name="context">Current context</param>
        /// <param name="type">Command type</param>
        /// <returns>Command input syntax</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null
        ///     <br/>or<br/>
        ///     <paramref name="type"/> is null
        /// </exception>
        protected static string MM__GetSyntax(IContext context, Type type)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(type);
            return null;
        }

        /// <summary>
        ///     <para>
        ///         Prepares the command for execution.
        ///     </para>
        ///     <para>
        ///         This is also where it should be checked for whether or not the user has requested help.<br/>
        ///         If so, display the help information, and set <see cref="PP_HelpRequest"/> to true.
        ///     </para>
        ///     <para>
        ///         If any invalid input is detected, a <see cref="CommandException"/> should be thrown.
        ///     </para>
        /// </summary>
        /// <param name="info">Command information</param>
        /// <param name="context">Current context</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="info"/> is null
        ///     <br/>or<br/>
        ///     <paramref name="context"/> is null
        /// </exception>
        /// <exception cref="CommandException">
        ///     Invalid input was detected
        /// </exception>
        protected internal abstract void MM_Prepare(ICmdInfo info, IContext context);

        /// <summary>
        ///     Main method
        /// </summary>
        /// <exception cref="CommandException">
        ///     An error occurred while executing the command
        /// </exception>
        public abstract void Main();

        #endregion
    }
}