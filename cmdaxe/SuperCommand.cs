using System;
using System.Collections.Immutable;
using System.Reflection;

namespace cmdaxe
{
    /// <summary>Represents a super command</summary>
    public abstract class SuperCommand : BaseCommand
    {
        #region fields

        private IContext f_Context = null;
        private ICmdGroup f_SubGroup = null;
        private bool f_HelpRequest = false;

        #endregion

        #region abstract properties

        /// <summary>Name of group containing subcommands</summary>
        protected abstract string PP_SubGroupName { get; }

        #endregion

        #region overridden properties

        /// <inheritdoc/>
        protected internal sealed override bool PP_HelpRequest => f_HelpRequest;

        #endregion

        #region overridden methods

        /// <summary>Gets the input syntax of the command</summary>
        /// <param name="context">Current context</param>
        /// <param name="type">Command type</param>
        /// <returns>Command input syntax</returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="context"/> is null
        ///     <br/>or<br/>
        ///     <paramref name="type"/> is null
        /// </exception>
        protected static new string MM__GetSyntax(IContext context, Type type)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(type);
            return "<subcommand>";
        }

        /// <inheritdoc/>
        protected internal sealed override void MM_Prepare(ICmdInfo info, IContext context)
        {
            ArgumentNullException.ThrowIfNull(info);
            ArgumentNullException.ThrowIfNull(context);
            // Get context
            f_Context = new InternalContext(
                (context.Original is null) ? context : context.Original,
                [..context.Prefixes, info.Name],
                context.RawInput);
            // Get group of subcommands
            if (!f_Context.CommandGroups.TryGet(PP_SubGroupName, out f_SubGroup))
                f_SubGroup = null;
            // Did user request help?
            f_HelpRequest = f_Context.RawInput.Length == 0 || info.IsHelp(f_Context.RawInput[0]);
            if (f_HelpRequest)
            {
                Console.WriteLine();
                Console.WriteLine(info.GetSyntax());
                Console.WriteLine(info.Desc);
                Console.WriteLine();
                if (f_SubGroup is not null)
                {
                    foreach (var command in f_SubGroup)
                    {
                        Console.WriteLine(command.GetSyntax(f_Context));
                        Console.WriteLine(command.Desc);
                        Console.WriteLine();
                    }
                }
                return;
            }
        }

        public sealed override void Main()
        {
            if (f_SubGroup is null) return;
            if (f_Context is null) return;
            f_SubGroup.MM_Run(f_Context, true);
        }

        #endregion
    }
}