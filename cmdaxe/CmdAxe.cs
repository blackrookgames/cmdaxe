using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace cmdaxe
{
    public static class CmdAxe
    { 
        #region nested

        private readonly struct StrOrNull(string value) : IEquatable<StrOrNull>, IComparable<StrOrNull>
        {
            #region object

            public override string ToString() => f_Value;

            public override bool Equals(object obj)
            {
                if (obj is null) return false;
                if (obj is not StrOrNull other) return false;
                return f_Value == other.f_Value;
            }

            public override int GetHashCode()
            {
                if (f_Value is null) return -1;
                return f_Value.GetHashCode();
            }

            #endregion

            #region IEquatable

            public bool Equals(StrOrNull other)
            {
                return f_Value == other.f_Value;
            }

            #endregion

            #region IComparable

            public int CompareTo(StrOrNull other)
            {
                if (f_Value is null)
                    return (other.f_Value is null) ? 0 : -1;
                if (other.f_Value is null)
                    return 1;
                return f_Value.CompareTo(other.f_Value);
            }

            #endregion

            #region fields

            private readonly string f_Value = value;

            #endregion

            #region properties

            /// <summary>String value</summary>
            public string Value => f_Value;

            #endregion
        }

        private class ParseFuncInfo : IParseFuncInfo
        {
            #region init

            /// <summary>
            /// Assume
            /// <br/>-<paramref name="type"/> is not null
            /// <br/>-<paramref name="attr"/> is not null
            /// <br/>-<paramref name="func"/> is not null
            /// <br/>-<paramref name="type"/> has the attribute <paramref name="attr"/>
            /// <br/>-<paramref name="func"/> parses to an instance of <paramref name="type"/>
            /// </summary>
            private ParseFuncInfo(Type type, ParseFuncAttribute attr, ParseFunc func)
            {
                f_Type = type;
                f_DisplayName = (attr.DisplayName is not null) ? attr.DisplayName : type.Name;
                f_Func = func;
            }

            public static bool TryParse(MethodInfo method, out ParseFuncInfo result)
            {
                result = null;
                if (method is null) return false;
                // Make sure method is static
                if (!method.IsStatic) return false;
                // Make sure method is valid
                ParseFunc func; 
                try { func = method.CreateDelegate<ParseFunc>(); }
                catch { return false; }
                // Find parse function attribute
                var attr = method.GetCustomAttribute<ParseFuncAttribute>();
                if (attr is null) return false;
                // Make sure type is defined
                if (attr.Type is null) return false;
                // Success!!!
                result = new ParseFuncInfo(attr.Type, attr, func);
                return true;
            }

            #endregion

            #region fields

            private readonly string f_DisplayName;
            private readonly Type f_Type;
            private readonly ParseFunc f_Func;

            #endregion

            #region properties

            /// <inheritdoc/>
            public Type Type => f_Type;

            /// <inheritdoc/>
            public string DisplayName => f_DisplayName;

            /// <inheritdoc/>
            public ParseFunc Func => f_Func;

            #endregion
        }

        private class ParseFuncs : IParseFuncs
        {
            #region fields

            private readonly Dictionary<Type, ParseFuncInfo> f_Items = [];

            #endregion

            #region properties

            /// <inheritdoc/>
            public int Count => f_Items.Count;

            /// <summary>Items dictionary</summary>
            public Dictionary<Type, ParseFuncInfo> Items => f_Items;

            #endregion

            #region methods

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public IEnumerator<IParseFuncInfo> GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public bool TryGet(Type type, out IParseFuncInfo func)
            {
                func = null;
                if (type is null)
                    return false;
                if (!f_Items.TryGetValue(type, out var raw))
                    return false;
                func = raw;
                return true;
            }

            #endregion
        }

        private class CmdInfoArgs
        {
            #region init

            /// <summary>
            /// Assume
            /// <br/>-<paramref name="type"/> is not null
            /// <br/>-<paramref name="attr"/> is not null
            /// <br/>-<paramref name="constructor"/> is not null
            /// <br/>-<paramref name="type"/> has the attribute <paramref name="attr"/>
            /// <br/>-<paramref name="constructor"/> creates an instance of  <paramref name="type"/>
            /// </summary>
            private CmdInfoArgs(Type type, CommandAttribute attr, ConstructorInfo constructor)
            {
                f_Name = (attr.Name is not null) ? attr.Name : type.Name;
                f_Type = type;
                f_Attr = attr;
                f_Constructor = constructor;
            }

            public static bool TryParse(Type type, out CmdInfoArgs result)
            {
                const BindingFlags FLAGS = 
                    BindingFlags.Instance | 
                    BindingFlags.Public | 
                    BindingFlags.NonPublic;
                result = null;
                // Is this a command type?
                if (type is null) return false;
                if (!type.IsSubclassOf(typeof(BaseCommand))) return false;
                // Find command attribute
                var attr = type.GetCustomAttribute<CommandAttribute>();
                if (attr is null) return false;
                // Find constructor
                var constructor = type.GetConstructor(FLAGS, []);
                if (constructor is null) return false;
                // Success!!!
                result = new CmdInfoArgs(type, attr, constructor);
                return true;
            }

            #endregion

            #region fields

            private readonly string f_Name;
            private readonly Type f_Type;
            private readonly CommandAttribute f_Attr;
            private readonly ConstructorInfo f_Constructor;

            #endregion

            #region properties

            public string Name => f_Name;

            public Type Type => f_Type;

            public CommandAttribute Attr => f_Attr;

            public ConstructorInfo Constructor => f_Constructor;

            #endregion
        }

        private class CmdInfo : ICmdInfo
        {
            #region init

            /// <summary>
            /// Assume
            /// <br/>-<paramref name="group"/> is not null
            /// <br/>-<paramref name="args"/> is not null
            /// </summary>
            public CmdInfo(CmdGroup group, CmdInfoArgs args)
            {
                f_Group = group;
                f_Type = args.Type;
                f_Name = args.Name;
                f_Desc = args.Attr.Desc;
                f_HelpKeyword = args.Attr.HelpKeyword;
                f_HelpShort = args.Attr.HelpShort;
                f_Constructor = args.Constructor;
            }

            #endregion

            #region fields

            private readonly CmdGroup f_Group;
            private readonly Type f_Type;
            private readonly string f_Name;
            private readonly string f_Desc;
            private readonly string f_HelpKeyword;
            private readonly char f_HelpShort;
            private readonly ConstructorInfo f_Constructor;

            #endregion

            #region properties

            /// <inheritdoc/>
            public ICmdGroup Group => f_Group;

            /// <inheritdoc/>
            public Type Type => f_Type;

            /// <inheritdoc/>
            public string Name => f_Name;

            /// <inheritdoc/>
            public string Desc => f_Desc;
            
            /// <inheritdoc/>
            public string HelpKeyword => f_HelpKeyword;

            /// <inheritdoc/>
            public char HelpShort => f_HelpShort;

            #endregion

            #region methods

            /// <inheritdoc/>
            BaseCommand ICmdInfo.MM_Instantiate()
            {
                return (BaseCommand)f_Constructor.Invoke([]);
            }

            #endregion
        }

        /// <summary>
        /// Assume
        /// <br/>-<paramref name="groups"/> is not null
        /// </summary>
        private class CmdGroup(ICmdGroups groups, string name) : ICmdGroup
        {
            #region fields

            private readonly ICmdGroups f_Groups = groups;
            private readonly string f_Name = name;
            private readonly Dictionary<string, ICmdInfo> f_Items = [];

            #endregion

            #region properties

            /// <inheritdoc/>
            public ICmdGroups Groups => f_Groups;

            /// <inheritdoc/>
            public string Name => f_Name;

            /// <inheritdoc/>
            public int Count => f_Items.Count;

            /// <summary>Items dictionary</summary>
            public Dictionary<string, ICmdInfo> Items => f_Items;

            #endregion

            #region methods

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public IEnumerator<ICmdInfo> GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public bool TryGet(string name, out ICmdInfo command)
            {
                if (name is null) { command = null; return false; }
                return f_Items.TryGetValue(name, out command);
            }

            #endregion
        }

        /// <summary>
        /// Assume
        /// <br/>-<paramref name="context"/> is not null
        /// </summary>
        private class CmdGroups(IContext context) : ICmdGroups
        {
            #region fields

            private readonly IContext f_Context = context;
            private readonly Dictionary<StrOrNull, CmdGroup> f_Items = [];

            #endregion

            #region properties

            /// <inheritdoc/>
            public IContext Context => f_Context;

            /// <inheritdoc/>
            public int Count => f_Items.Count;

            /// <summary>Items dictionary</summary>
            public Dictionary<StrOrNull, CmdGroup> Items => f_Items;

            #endregion

            #region methods

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public IEnumerator<ICmdGroup> GetEnumerator() => f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public bool TryGet(string name, out ICmdGroup command)
            {
                if (f_Items.TryGetValue(new(name), out var raw))
                {
                    command = raw;
                    return true;
                }
                else
                {
                    command = null;
                    return false;
                }
            }

            #endregion
        }

        private class Context : IContext
        {
            #region init

            public Context(string entryName, ImmutableArray<string> rawInput)
            {
                f_EntryName = entryName;
                f_CommandGroups = new(this);
                f_ParseFuncs = new();
                f_Prefixes = [];
                f_RawInput = rawInput;
            }

            #endregion

            #region IContext
            
            /// <inheritdoc/>
            public IContext Original => null;

            /// <inheritdoc/>
            public string EntryName => f_EntryName;

            ICmdGroups IContext.CommandGroups => f_CommandGroups;

            IParseFuncs IContext.ParseFuncs => f_ParseFuncs;

            /// <inheritdoc/>
            public ImmutableArray<string> Prefixes => f_Prefixes;

            /// <inheritdoc/>
            public ImmutableArray<string> RawInput => f_RawInput;

            #endregion

            #region const

            private static readonly ImmutableArray<string> EMPTY = [];

            #endregion

            #region fields

            private readonly string f_EntryName;
            private readonly CmdGroups f_CommandGroups;
            private readonly ParseFuncs f_ParseFuncs;
            private readonly ImmutableArray<string> f_Prefixes;
            private readonly ImmutableArray<string> f_RawInput;

            #endregion

            #region properties

            public CmdGroups CommandGroups => f_CommandGroups;

            public ParseFuncs ParseFuncs => f_ParseFuncs;

            #endregion
        }

        #endregion

        #region helper methods

        private static Assembly MM_GetEntryAssembly()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly is null) throw new CmdAxeException("Entry assembly could not be retrieved.");
            return assembly;
        }

        /// <summary>
        ///     Assume
        ///     <br/>- <paramref name="assembly"/> is not null
        /// </summary>
        private static string MM_GetEntryName(Assembly assembly)
        {
            try
            {
                var currentDir = Path.GetFullPath(Directory.GetCurrentDirectory());
                return Path.GetRelativePath(currentDir, Path.GetFullPath(Environment.ProcessPath));
            }
            catch { return assembly.GetName().Name; }
        }

        /// <summary>
        ///     Assume
        ///     <br/>- <paramref name="context"/> is not null
        /// </summary>
        private static ICmdGroup MM_GetGroup(IContext context, string group)
        {
            if (context.CommandGroups.TryGet(group, out var cmdGroup)) return cmdGroup;
            throw new KeyNotFoundException("Could not find a group of the specified name.");
        }

        #endregion

        #region methods

        /// <summary>Creates a <see cref="cmdaxe"/> context</summary>
        /// <param name="input">User input</param>
        /// <param name="assembly">Entry assembly</param>
        /// <param name="entryName">Entry name</param>
        /// <returns>Created context</returns>
        /// <exception cref="CmdAxeException">
        ///     <paramref name="assembly"/> is null and entry assembly could not be retrieved
        ///     <br/>or<br/>
        ///     An error occurred when retrieving types defined in <paramref name="assembly"/>
        ///     <br/>or<br/>
        ///     Two or more commands in the same group have the same name
        ///     <br/>or<br/>
        ///     Two or more parse functions have the same target type
        /// </exception>
        public static IContext CreateContext(IEnumerable<string> input, 
            Assembly assembly = null, string entryName = null)
        {
            const BindingFlags METHOD_FLAGS = 
                BindingFlags.Static | 
                BindingFlags.Public | 
                BindingFlags.NonPublic;
            assembly ??= MM_GetEntryAssembly();
            entryName ??= MM_GetEntryName(assembly);
            // Get types
            Type[] types;
            try
            { types = assembly.GetTypes(); }
            catch (ReflectionTypeLoadException e)
            { throw new CmdAxeException("Failed to retrieve the types defined in the assembly.", e); }
            // Create context
            var context = new Context(entryName, (input is null) ? [] : [..input]);
            foreach (var type in types)
            {
                // Is this a command?
                if (CmdInfoArgs.TryParse(type, out var args))
                {
                    // Find/create group
                    var groupName = new StrOrNull(args.Attr.Group);
                    if (!context.CommandGroups.Items.TryGetValue(groupName, out var group))
                    {
                        group = new CmdGroup(context.CommandGroups, args.Attr.Group);
                        context.CommandGroups.Items.Add(groupName, group);
                    }
                    // Make sure command name is not taken
                    if (group.Items.ContainsKey(args.Name))
                        throw new CmdAxeException("Two or more commands in the same group have the same name.");
                    // Add command
                    var info = new CmdInfo(group, args);
                    group.Items.Add(info.Name, info);
                }
                // Look for parse functions
                foreach (var method in type.GetMethods(METHOD_FLAGS))
                {
                    // Make sure this is a valid parse function
                    if (!ParseFuncInfo.TryParse(method, out var func))
                        continue;
                    // Make sure the target type does not already exists in collection
                    if (context.ParseFuncs.Items.ContainsKey(func.Type))
                        throw new CmdAxeException("Two or more parse functions have the same target type.");
                    // Add function
                    context.ParseFuncs.Items.Add(func.Type, func);
                }
            }
            // Add missing parse functions for default types
            foreach (var method in typeof(InternalParsing).GetMethods(METHOD_FLAGS))
            {
                // Make sure this is a valid parse function
                if (!ParseFuncInfo.TryParse(method, out var func))
                    continue;
                // Skip if user has already defined a function for the target type
                if (context.ParseFuncs.Items.ContainsKey(func.Type))
                    continue;
                // Add function
                context.ParseFuncs.Items.Add(func.Type, func);
            }
            return context;
        }
        
        /// <summary>Main method</summary>
        /// <param name="input">User input</param>
        /// <param name="assembly">Entry assembly</param>
        /// <param name="entryName">Entry name</param>
        /// <param name="group">Group of valid commands</param>
        /// <returns>Created context</returns>
        /// <exception cref="CmdAxeException">
        ///     <paramref name="assembly"/> is null and entry assembly could not be retrieved
        ///     <br/>or<br/>
        ///     An error occurred when retrieving types defined in <paramref name="assembly"/>
        ///     <br/>or<br/>
        ///     Two or more commands in the same group have the same name
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        ///     Could not find a group of the specified name
        /// </exception>
        public static int Run(IEnumerable<string> input, 
            Assembly assembly = null, string entryName = null, string group = null)
        {
            var context = CreateContext(input, assembly, entryName);
            return MM_GetGroup(context, group).Run();
        }
        
        #endregion
    }
}