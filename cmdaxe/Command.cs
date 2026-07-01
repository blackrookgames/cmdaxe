using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            int i, j;
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
                    IOption? option = null;
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
            f_HelpRequest = allParams.Required.Count > 0 && required.Count == 0;
            foreach (var flag in optionFlags)
            {
                allParams.Optional.TryGet(flag, out var flagInfo);
                if (((IOptionFlag)flagInfo!).IsHelp) f_HelpRequest = true;
            }
            if (f_HelpRequest)
            {
                Console.WriteLine();
                Console.WriteLine(info.GetSyntax());
                Console.WriteLine(info.Desc);
                Console.WriteLine();
                // Determine display width
                int rawWidth;
                try { rawWidth = Console.BufferWidth; }
                catch
                {
                    try { rawWidth = Console.WindowWidth; }
                    catch { rawWidth = 80; }
                }
                var displayWidth = rawWidth - 1;
                // Determine name column data
                var params_Obj = new IParameter[allParams.Count];
                var params_Name = new string[allParams.Count, 2];
                i = 0;
                foreach (var param in allParams)
                {
                    if (param is not IOptionFlag flag) continue;
                    if (!flag.IsHelp) continue;
                    params_Obj[i] = param;
                    j = 0;
                    if (flag.Name != "")
                        params_Name[i, j++] = $"--{flag.Name}";
                    if (flag.Shortcut != '\0')
                        params_Name[i, j++] = $"-{flag.Shortcut}";
                    ++i;
                }
                foreach (var param in allParams)
                {
                    if (param is IRequired req)
                    {
                        params_Name[i, 0] = $"<{req.Name}>";
                    }
                    else if (param is IOptionFlag optFlag)
                    {
                        if (optFlag.IsHelp) continue;
                        params_Name[i, 0] = $"--{optFlag.Name}";
                        if (optFlag.Shortcut != '\0')
                            params_Name[i, 1] = $"-{optFlag.Shortcut}";
                    }
                    else if (param is IOptionWArg optWArg)
                    {
                        string argName = string.IsNullOrEmpty(optWArg.ArgType) ? "value" : optWArg.ArgType;
                        params_Name[i, 0] = $"--{optWArg.Name} <{argName}>";
                        if (optWArg.Shortcut != '\0')
                            params_Name[i, 1] = $"-{optWArg.Shortcut} <{argName}>";
                    }
                    params_Obj[i] = param;
                    ++i;
                }
                var nameWidth = 0;
                for (i = 0; i < allParams.Count; ++i)
                {
                    for (j = 0; j < 2; ++j)
                    {
                        var name = params_Name[i, j];
                        if (name is not null && nameWidth < name.Length)
                            nameWidth = name.Length;
                    }
                }
                nameWidth += 4;
                var noName = new string(' ', nameWidth);
                // Determine description column width
                var descWidth = displayWidth - nameWidth;
                if (descWidth <= 0) return;
                // Print parameters
                for (i = 0; i < allParams.Count; ++i)
                {
                    var param = params_Obj[i];
                    // Create name column
                    var nameLines = new List<string>();
                    for (j = 0; j < 2; ++j)
                    {
                        var name = params_Name[i, j];
                        if (name is not null)
                            nameLines.Add(name);
                    }
                    // Create description column
                    var descLines = new List<string>();
                    if (param.Desc is not null)
                    {
                        const int LONGWORD = 15;
                        // Create final description
                        var desc = param.Desc;
                        if (param is IOptionWArg optWArg && optWArg.IsArray)
                            desc += "\nThis option can be specified multiple times.";
                        // Create lines
                        var sb = new StringBuilder();
                        j = 0;
                        while (j < desc.Length)
                        {
                            char c = desc[j];
                            // Non-whitespace
                            if (c > ' ')
                            {
                                // Find end of word
                                int k = j + 1;
                                while (k < desc.Length)
                                {
                                    c = desc[k];
                                    if (c <= ' ') break;
                                    ++k;
                                }
                                int len = k - j;
                                // Is word too long for current line?
                                if ((sb.Length + len) > descWidth)
                                {
                                    // Is word really long?
                                    if (len > descWidth || len >= LONGWORD)
                                    {
                                        // Is the description width only 1 character?
                                        if (descWidth == 1)
                                        {
                                            for (int l = j; l < k; ++l)
                                            {
                                                if (sb.Length >= descWidth)
                                                {
                                                    descLines.Add(sb.ToString());
                                                    sb.Clear();
                                                }
                                                sb.Append(desc[l]);
                                            }
                                        }
                                        // No!
                                        else
                                        {
                                            int l = j;
                                            if (sb.Length > 0)
                                            {
                                                // Should we use what's left of the current line?
                                                int whatsLeft = descWidth - sb.Length;
                                                if (whatsLeft > 2)
                                                {
                                                    l += whatsLeft - 1;
                                                    sb.Append(desc[j..l]);
                                                    sb.Append('-');
                                                }
                                                // New line
                                                descLines.Add(sb.ToString());
                                                sb.Clear();
                                            }
                                            while (l < k)
                                            {
                                                // Is there room for the rest of the word?
                                                if ((k - l) <= descWidth)
                                                {
                                                    sb.Append(desc[l..k]);
                                                    l = k;
                                                }
                                                // No!
                                                else
                                                {
                                                    int m = l + descWidth - 1;
                                                    sb.Append(desc[l..m]);
                                                    sb.Append('-');
                                                    // New line
                                                    descLines.Add(sb.ToString());
                                                    sb.Clear();
                                                    // Next
                                                    l = m;
                                                }
                                            }
                                        }
                                    }
                                    // No!
                                    else
                                    {
                                        // New line
                                        descLines.Add(sb.ToString());
                                        sb.Clear();
                                        // Add word to line
                                        sb.Append(desc[j..k]);
                                    }
                                }
                                // No!
                                else
                                {
                                    // Add word to line
                                    sb.Append(desc[j..k]);
                                }
                                // Next
                                j = k;
                            }
                            // Whitespace
                            else
                            {
                                while (j < desc.Length)
                                {
                                    c = desc[j];
                                    // Is this non-whitespace?
                                    if (c > ' ') break;
                                    // Add to line
                                    if (sb.Length < descWidth) // The end of the line absorbs excess whitespace
                                    {
                                        // Is this a new line?
                                        if (c == '\n')
                                        {
                                            descLines.Add(sb.ToString());
                                            sb.Clear();
                                        }
                                        // No!
                                        else
                                        {
                                            sb.Append(c);
                                        }
                                    }
                                    // Next
                                    ++j;
                                }
                                if (sb.Length >= descWidth)
                                {
                                    descLines.Add(sb.ToString());
                                    sb.Clear();
                                }
                            }
                        }
                        if (sb.Length > 0)
                            descLines.Add(sb.ToString());
                    }
                    // Print
                    int height = Math.Max(nameLines.Count, descLines.Count);
                    for (j = 0; j < height; ++j)
                    {
                        // Name
                        if (j < nameLines.Count)
                            Console.Write(nameLines[j].PadRight(nameWidth));
                        else
                            Console.Write(noName);
                        // Desc
                        if (j < descLines.Count)
                            Console.Write(descLines[j]);
                        // End of line
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
                // Success!!!
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
                    for (j = 0; j < array.Length; ++j)
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
                flagInfo!.SetValue(this, true);
            }
            foreach (var kvp in optionWArgs)
            {
                allParams.Optional.TryGet(kvp.Key, out var rawOption);
                IOptionWArg option = (IOptionWArg)rawOption!;
                if (option.IsArray)
                {
                    var array = Array.CreateInstance(option.ParseFunc.Type, kvp.Value.Count);
                    for (j = 0; j < array.Length; ++j)
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
        private static object? MM_Parse(IParseFuncInfo func, string input)
        {
            if (func.Func(input, out var result)) return result;
            throw new CommandException($"\"{input}\" is not a valid {func.DisplayName} value.");
        }

        #endregion
    }
}