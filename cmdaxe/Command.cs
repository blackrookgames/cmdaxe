using System;
using System.Collections.Generic;
using System.IO;

namespace cmdaxe
{
    /// <summary>Represents an executable command</summary>
    public abstract class Command : BaseCommand
    {
        #region fields

        private bool f_HelpRequest = false;

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
            var allParams = new InternalParameters(type, context.ParseFuncs);
            using StringWriter w = new();
            // Optional
            var hasOptions = allParams.Optional.Count > 0;
            if (hasOptions) w.Write("[<OPTIONS>]");
            // Required
            bool notFirst = false;
            foreach (var param in allParams.Required)
            {
                if (notFirst || hasOptions) w.Write(' ');
                if (param.IsArray) w.Write($"[<{param.Name}0> <{param.Name}1> ...]");
                else w.Write($"<{param.Name}>");
                notFirst = true;
            }
            // Success!!!
            return w.ToString();
        }

        /// <inheritdoc/>
        protected internal sealed override void MM_Prepare(ICmdInfo info, IContext context)
        {
            ArgumentNullException.ThrowIfNull(info);
            ArgumentNullException.ThrowIfNull(context);
            int i;
            // Get parameters
            var allParams = new InternalParameters(GetType(), context.ParseFuncs);
            // Gather input
            List<string> required = [];
            HashSet<string> optionFlags = [];
            Dictionary<string, List<string>> optionWArgs = [];
            bool lookForOptions = true;
            i = 0;
            while (i < context.RawInput.Length)
            {
                var input = context.RawInput[i];
                input ??= "";
                // Is this an option?
                if (lookForOptions && input.Length >= 2 && input[0] == '-')
                {
                    IOption option = null;
                    // Is this a shortcut?
                    if (input.Length == 2)
                    {
                        char c = input[1];
                        // Is the shortcut valid?
                        if (allParams.Shortcuts.TryGet(c, out option)) { }
                        // No! Does this indicate the end of options?
                        else if (c == '-') lookForOptions = false;
                        // No!
                        else throw new CommandException($"Unknown shortcut: {input}");
                    }
                    // No! Is this a valid keyword?
                    else if (input[1] == '-')
                    {
                        // Is the keyword valid?
                        if (allParams.Optional.TryGet(input[2..], out option)) { }
                        // No!
                        else throw new CommandException($"Unknown option: {input}");
                    }
                    else throw new CommandException($"Invalid option syntax: {input}");
                    // Evaluate option
                    if (option is not null)
                    {
                        // Is the option a flag?
                        if (option is IOptionFlag)
                        {
                            optionFlags.Add(option.Name);
                        }
                        // No!
                        else
                        {
                            if (++i >= context.RawInput.Length)
                                throw new CommandException($"Expected an argument for {input}");
                            if (!optionWArgs.TryGetValue(option.Name, out var args))
                            {
                                args = [];
                                optionWArgs.Add(option.Name, args);
                            }
                            args.Add(context.RawInput[i]);
                        }
                    }
                }
                // No!
                else
                {
                    required.Add(input);
                }
                // Next
                ++i;
            }
            // Check if user requested help
            f_HelpRequest = false;
            foreach (var flag in optionFlags)
            {
                allParams.Optional.TryGet(flag, out var flagInfo);
                f_HelpRequest = ((IOptionFlag)flagInfo).IsHelp;
            }
            if (f_HelpRequest)
            {
                Console.WriteLine();
                Console.WriteLine(info.GetSyntax());
                Console.WriteLine(info.Desc);
                Console.WriteLine();
                // TODO: Print parameters
                return;
            }
            // Parse required
            int reqCount = 0;
            i = 0;
            foreach (var reqParam in allParams.Required)
            {
                if (reqParam.IsArray)
                {
                    var array = Array.CreateInstance(reqParam.ParseFunc.Type, required.Count - i);
                    for (var j = 0; j < array.Length; ++j)
                    {
                        var input = required[i++];
                        var value = MM_Parse(reqParam.ParseFunc, input);
                        array.SetValue(value, j);
                    }
                    reqParam.SetValue(this, array);
                }
                else
                {
                    if (i >= required.Count) break;
                    var input = required[i++];
                    var value = MM_Parse(reqParam.ParseFunc, input);
                    reqParam.SetValue(this, value);
                }
                ++reqCount;
            }
            if (reqCount < allParams.Required.Count)
                throw new CommandException("Not all required arguments have been given.");
            // Parse optional
            foreach (var flag in optionFlags)
            {
                allParams.Optional.TryGet(flag, out var flagInfo);
                flagInfo.SetValue(this, true);
            }
            foreach (var kvp in optionWArgs)
            {
                allParams.Optional.TryGet(kvp.Key, out var rawOption);
                IOptionWArg option = (IOptionWArg)rawOption;
                if (option.IsArray)
                {
                    var array = Array.CreateInstance(option.ParseFunc.Type, kvp.Value.Count);
                    for (var j = 0; j < array.Length; ++j)
                    {
                        var value = MM_Parse(option.ParseFunc, kvp.Value[j]);
                        array.SetValue(value, j);
                    }
                    option.SetValue(this, array);
                }
                else
                {
                    var value = MM_Parse(option.ParseFunc, kvp.Value[^1]);
                    option.SetValue(this, value);
                }
            }
            
        }

        #endregion

        #region helper methods

        /// <summary>
        ///     Assume
        ///     <br/>- <paramref name="func"/> is not null
        /// </summary>
        private static object MM_Parse(IParseFuncInfo func, string input)
        {
            if (func.Func(input, out var result)) return result;
            throw new CommandException($"\"{input}\" is not a valid {func.DisplayName} value.");
        }

        #endregion
    }
}