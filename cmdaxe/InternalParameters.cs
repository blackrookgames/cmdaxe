using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace cmdaxe
{
    /// <summary>Represents command-line parameters</summary>
    internal class InternalParameters : IAllParameters
    {
        #region parameter classes

        private delegate void SetValue(object instance, object? value);

        private class EnumParseFunc : IParseFuncInfo
        {
            #region init

            /// <summary>
            /// Assume
            /// <br/>-<paramref name="type"/> is not null
            /// <br/>-<paramref name="type"/> is an enumeration
            /// </summary>
            private EnumParseFunc(Type type)
            {
                f_Type = type;
                f_DisplayName = f_Type.Name;
                f_Func = MM_TryParse;
            }

            public static bool TryParse(Type type, out EnumParseFunc? result)
            {
                result = null;
                if (type is null) return false;
                // Make sure type is enum
                if (!type.IsEnum) return false;
                // Success!!!
                result = new EnumParseFunc(type);
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

            #region helper methods

            private bool MM_TryParse(string? input, out object? result) =>
                Enum.TryParse(f_Type, input, false, out result);

            #endregion
        }

        private class FieldOrProp
        {
            #region init

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="name"/> is not null
            ///     <br/>- <paramref name="type"/> is not null
            ///     <br/>- <paramref name="attr"/> is not null
            ///     <br/>- <paramref name="setValue"/> is not null
            /// </summary>
            private FieldOrProp(
                string name,
                string? desc,
                Type type,
                bool isArray,
                ParameterAttribute attr,
                SetValue setValue)
            {
                f_Name = name;
                f_Desc = desc;
                f_SetValue = setValue;
                f_Type = type;
                f_IsArray = isArray;
                f_Attr = attr;
            }
            
            public static bool TryParse(FieldInfo? field, out FieldOrProp? result)
            {
                result = default;
                if (field is null) return false;
                // Get attribute
                var attr = field.GetCustomAttribute<ParameterAttribute>();
                if (attr is null) return false;
                // Generate set action
                void setValue(object instance, object? value)
                {
                    ArgumentNullException.ThrowIfNull(instance);
                    field.SetValue(instance, value);
                }
                // Get target type
                Type type = field.FieldType.IsArray ? 
                    field.FieldType.GetElementType()! : 
                    field.FieldType;
                // Success
                result = new FieldOrProp(
                    MM_GetName(attr, field.Name),
                    attr.Desc,
                    type,
                    field.FieldType.IsArray,
                    attr,
                    setValue);
                return true;
            }
            
            public static bool TryParse(PropertyInfo? property, out FieldOrProp? result)
            {
                result = default;
                if (property is null) return false;
                // Get attribute
                var attr = property.GetCustomAttribute<ParameterAttribute>();
                if (attr is null) return false;
                // Get set method
                var setMethod = property.SetMethod;
                if (setMethod is null) return false;
                // Generate set action
                void setValue(object instance, object? value)
                {
                    ArgumentNullException.ThrowIfNull(instance);
                    setMethod.Invoke(instance, [value]);
                }
                // Get target type
                Type type = property.PropertyType.IsArray ? 
                    property.PropertyType.GetElementType()! : 
                    property.PropertyType;
                // Success
                result = new FieldOrProp(
                    MM_GetName(attr, property.Name),
                    attr.Desc,
                    type, 
                    property.PropertyType.IsArray, 
                    attr, 
                    setValue);
                return true;
            }

            #endregion

            #region fields
            
            private readonly string f_Name;

            private readonly string? f_Desc;
            
            private readonly SetValue f_SetValue;

            private readonly Type f_Type;

            private readonly bool f_IsArray;

            private readonly ParameterAttribute f_Attr;

            #endregion

            #region properties

            /// <summary>Parameter name</summary>
            public string Name => f_Name;

            /// <summary>Parameter desc</summary>
            public string? Desc => f_Desc;
            
            /// <summary>Action for setting the field or property</summary>
            public SetValue SetValue => f_SetValue;

            /// <summary>
            ///     <para>Target type</para>
            ///     <para>If the field or property is an array, this is the element type</para>
            /// </summary>
            public Type Type => f_Type;

            /// <summary>Whether or not the field or property is an array</summary>
            public bool IsArray => f_IsArray;

            /// <summary>Attribute</summary>
            public ParameterAttribute Attr => f_Attr;

            #endregion

            #region helper methods

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="attr"/> is not null
            ///     <br/>- <paramref name="fallbackName"/> is not null
            /// </summary>
            private static string MM_GetName(ParameterAttribute attr, string fallbackName)
            {
                if (string.IsNullOrEmpty(attr.Name))
                    return fallbackName;
                return attr.Name;
            }

            #endregion
        }

        private class ReqParam : IRequired
        {
            #region init

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            ///     <br/>- <paramref name="parseFunc"/> is not null
            /// </summary>
            private ReqParam(FieldOrProp fieldOrProp, IParseFuncInfo parseFunc)
            {
                f_FieldOrProp = fieldOrProp;
                f_ParseFunc = parseFunc;
            }

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            ///     <br/>- <paramref name="parseFuncs"/> is not null
            /// </summary>
            public static bool TryParse(FieldOrProp fieldOrProp, IParseFuncs parseFuncs, out ReqParam? result)
            {
                result = null;
                // Check if attribute is valid
                if (fieldOrProp.Attr is not RequiredAttribute)
                    return false;
                // Check if type is valid
                if (!MM_TryGetParseFunc(fieldOrProp.Type, parseFuncs, out var parseFunc))
                    return false;
                // Success!!!
                result = new ReqParam(fieldOrProp, parseFunc!);
                return true;
            }

            #endregion

            #region fields
            
            private readonly FieldOrProp f_FieldOrProp;
            private readonly IParseFuncInfo f_ParseFunc;

            #endregion

            #region properties

            /// <inheritdoc/>
            public string Name => f_FieldOrProp.Name;

            /// <inheritdoc/>
            public string? Desc => f_FieldOrProp.Desc;

            /// <inheritdoc/>
            public bool IsArray => f_FieldOrProp.IsArray;
            
            /// <inheritdoc/>
            public IParseFuncInfo ParseFunc => f_ParseFunc;

            #endregion

            #region methods

            /// <inheritdoc/>
            public void SetValue(object instance, object? value) => 
                f_FieldOrProp.SetValue(instance, value);

            #endregion
        }

        private class OptParamFlag : IOptionFlag
        {
            #region init

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            /// </summary>
            private OptParamFlag(FieldOrProp fieldOrProp, char shortcut)
            {
                f_FieldOrProp = fieldOrProp;
                f_Shortcut = shortcut;
            }

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            /// </summary>
            public static bool TryParse(FieldOrProp fieldOrProp, out OptParamFlag? result)
            {
                result = null;
                // Check if attribute is valid
                if (fieldOrProp.Attr is not OptionFlagAttribute attr)
                    return false;
                // Check if type is valid
                if (fieldOrProp.Type != typeof(bool))
                    return false;
                // Success!!!
                result = new OptParamFlag(fieldOrProp, attr.Shortcut);
                return true;
            }

            #endregion

            #region fields
            
            private readonly FieldOrProp f_FieldOrProp;
            private readonly char f_Shortcut;

            #endregion

            #region properties

            /// <inheritdoc/>
            public string Name => f_FieldOrProp.Name;

            /// <inheritdoc/>
            public char Shortcut => f_Shortcut;

            /// <inheritdoc/>
            public string? Desc => f_FieldOrProp.Desc;

            /// <inheritdoc/>
            public bool IsHelp => false;

            #endregion

            #region methods

            /// <inheritdoc/>
            public void SetValue(object instance, object? value) => 
                f_FieldOrProp.SetValue(instance, value);

            #endregion
        }

        private class OptParamWArg : IOptionWArg
        {
            #region init

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            ///     <br/>- <paramref name="parseFunc"/> is not null
            /// </summary>
            private OptParamWArg(FieldOrProp fieldOrProp, char shortcut, IParseFuncInfo parseFunc, string? argType)
            {
                f_FieldOrProp = fieldOrProp;
                f_Shortcut = shortcut;
                f_ParseFunc = parseFunc;
                f_ArgType = argType;
            }

            /// <summary>
            ///     Assume
            ///     <br/>- <paramref name="fieldOrProp"/> is not null
            ///     <br/>- <paramref name="parseFuncs"/> is not null
            /// </summary>
            public static bool TryParse(FieldOrProp fieldOrProp, IParseFuncs parseFuncs, out OptParamWArg? result)
            {
                result = null;
                // Check if attribute is valid
                if (fieldOrProp.Attr is not OptionWArgAttribute attr)
                    return false;
                // Check if type is valid
                if (!MM_TryGetParseFunc(fieldOrProp.Type, parseFuncs, out var parseFunc))
                    return false;
                // Success!!!
                result = new OptParamWArg(fieldOrProp, attr.Shortcut, parseFunc!, attr.ArgType);
                return true;
            }

            #endregion

            #region fields
            
            private readonly FieldOrProp f_FieldOrProp;
            private readonly char f_Shortcut;
            private readonly IParseFuncInfo f_ParseFunc;
            private readonly string? f_ArgType;

            #endregion

            #region properties

            /// <inheritdoc/>
            public string Name => f_FieldOrProp.Name;

            /// <inheritdoc/>
            public char Shortcut => f_Shortcut;

            /// <inheritdoc/>
            public string? Desc => f_FieldOrProp.Desc;

            /// <inheritdoc/>
            public bool IsArray => f_FieldOrProp.IsArray;
            
            /// <inheritdoc/>
            public IParseFuncInfo ParseFunc => f_ParseFunc;

            /// <inheritdoc/>
            public string? ArgType => f_ArgType;

            #endregion

            #region methods

            /// <inheritdoc/>
            public void SetValue(object instance, object? value) => 
                f_FieldOrProp.SetValue(instance, value);

            #endregion
        }

        private class HelpParam(string? name, char shortcut) : IOptionFlag
        {
            #region fields

            private readonly string f_Name = (name is null) ? "" : name;
            private readonly char f_Shortcut = shortcut;
            private readonly string f_Desc = "Displays help information";

            #endregion

            #region properties

            /// <inheritdoc/>
            public string Name => f_Name;

            /// <inheritdoc/>
            public char Shortcut => f_Shortcut;

            /// <inheritdoc/>
            public string Desc => f_Desc;

            /// <inheritdoc/>
            public bool IsHelp => true;

            #endregion

            #region methods

            /// <inheritdoc/>
            public void SetValue(object instance, object? value) { }

            #endregion
        }

        #endregion

        #region parameter collection classes

        private class Parameters<T> : IParameters<T> where T : IParameter
        {
            #region IParameters

            /// <inheritdoc/>
            public int Count => f_Items.Count;

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() =>
                f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public IEnumerator<T> GetEnumerator() => 
                f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public bool TryGet(string? name, out T? func)
            {
                if (name is not null)
                    return f_Items.TryGetValue(name, out func);
                func = default;
                return false;
            }

            #endregion

            #region fields

            private Dictionary<string, T> f_Items = [];

            #endregion

            #region properties

            public Dictionary<string, T> Items => f_Items;

            #endregion
        }

        private class OptionsWithShortcut : IOptionsWithShortcut
        {
            #region IOptionsWithShortcut

            /// <inheritdoc/>
            public int Count => f_Items.Count;

            /// <inheritdoc/>
            IEnumerator IEnumerable.GetEnumerator() =>
                f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public IEnumerator<IOption> GetEnumerator() => 
                f_Items.Values.GetEnumerator();

            /// <inheritdoc/>
            public bool TryGet(char shortcut, out IOption? func) =>
                f_Items.TryGetValue(shortcut, out func);

            #endregion

            #region fields

            private Dictionary<char, IOption> f_Items = [];

            #endregion

            #region properties

            public Dictionary<char, IOption> Items => f_Items;

            #endregion
        }

        #endregion

        #region init

        /// <summary>Initializer for <see cref="InternalParameters"/></summary>
        /// <param name="type">Command type</param>
        /// <param name="parseFuncs">Parse functions</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type"/> is null
        ///     <br/>or<br/>
        ///     <paramref name="parseFuncs"/> is null
        /// </exception>
        public InternalParameters(Type type, IParseFuncs parseFuncs)
        {
            const BindingFlags BINDING_FLAGS = 
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic;
            ArgumentNullException.ThrowIfNull(type);
            ArgumentNullException.ThrowIfNull(parseFuncs);
            // Get help flag
            HelpParam? helpParam = null;
            var attr = type.GetCustomAttribute<CommandAttribute>();
            if (attr is not null && ((!string.IsNullOrEmpty(attr.HelpKeyword)) || attr.HelpShort != '\0'))
                helpParam = new(attr.HelpKeyword, attr.HelpShort);
            // Gather all parameters
            var reqParams = new List<IRequired>();
            var optParams = new List<IOption>();
            foreach (var fieldInfo in type.GetFields(BINDING_FLAGS))
            {
                if (!FieldOrProp.TryParse(fieldInfo, out var field))
                    continue;
                if (ReqParam.TryParse(field!, parseFuncs, out var required))
                    reqParams.Add(required!);
                else if (OptParamFlag.TryParse(field!, out var optionFlag))
                    optParams.Add(optionFlag!);
                else if (OptParamWArg.TryParse(field!, parseFuncs, out var optionWArg))
                    optParams.Add(optionWArg!);
            }
            foreach (var propertyInfo in type.GetProperties(BINDING_FLAGS))
            {
                if (!FieldOrProp.TryParse(propertyInfo, out var property))
                    continue;
                if (ReqParam.TryParse(property!, parseFuncs, out var required))
                    reqParams.Add(required!);
                else if (OptParamFlag.TryParse(property!, out var optionFlag))
                    optParams.Add(optionFlag!);
                else if (OptParamWArg.TryParse(property!, parseFuncs, out var optionWArg))
                    optParams.Add(optionWArg!);
            }
            // Create collection
            f_Items = [];
            f_Required = new();
            f_Optional = new();
            f_Shortcuts = new();
            IRequired? reqArray = null; // Save this one for last
            if (helpParam is not null)
            {
                // Add
                f_Items.Add(helpParam.Name, helpParam);
                f_Optional.Items.Add(helpParam.Name, helpParam);
                // Add shortcut
                if (helpParam.Shortcut != '\0' && (!f_Shortcuts.Items.ContainsKey(helpParam.Shortcut)))
                    f_Shortcuts.Items.Add(helpParam.Shortcut, helpParam);
            }
            foreach (var param in reqParams)
            {
                // Is this an array?
                if (param.IsArray)
                { if (reqArray is not null) continue; }
                // Is the name taken?
                if (f_Items.ContainsKey(param.Name)) continue;
                // Add
                f_Items.Add(param.Name, param);
                if (param.IsArray) reqArray = param;
                else f_Required.Items.Add(param.Name, param);
            }
            foreach (var param in optParams)
            {
                // Is the name taken?
                if (f_Items.ContainsKey(param.Name)) continue;
                // Add
                f_Items.Add(param.Name, param);
                f_Optional.Items.Add(param.Name, param);
                // Add shortcut
                if (param.Shortcut != '\0' && (!f_Shortcuts.Items.ContainsKey(param.Shortcut)))
                    f_Shortcuts.Items.Add(param.Shortcut, param);
            }
            if (reqArray is not null)
                f_Required.Items.Add(reqArray.Name, reqArray);
        }

        #endregion

        #region IAllParameters

        /// <inheritdoc/>
        public int Count => f_Items.Count;

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() =>
            f_Items.Values.GetEnumerator();

        /// <inheritdoc/>
        public IEnumerator<IParameter> GetEnumerator() => 
            f_Items.Values.GetEnumerator();

        /// <inheritdoc/>
        public bool TryGet(string? name, out IParameter? func)
        {
            if (name is not null)
                return f_Items.TryGetValue(name, out func);
            func = default;
            return false;
        }

        /// <inheritdoc/>
        public IParameters<IRequired> Required => f_Required;

        /// <inheritdoc/>
        public IParameters<IOption> Optional => f_Optional;

        /// <inheritdoc/>
        public IOptionsWithShortcut Shortcuts => f_Shortcuts;

        #endregion

        #region fields

        private readonly Dictionary<string, IParameter> f_Items;
        private readonly Parameters<IRequired> f_Required;
        private readonly Parameters<IOption> f_Optional;
        private readonly OptionsWithShortcut f_Shortcuts;

        #endregion

        #region helper methods

        /// <summary>
        ///     Assume
        ///     <br/>-<paramref name="type"/> is not null
        ///     <br/>-<paramref name="parseFuncs"/> is not null
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parseFuncs"></param>
        /// <param name="parseFunc"></param>
        /// <returns></returns>
        private static bool MM_TryGetParseFunc(Type type, IParseFuncs parseFuncs, out IParseFuncInfo? parseFunc)
        {
            if (parseFuncs.TryGet(type, out parseFunc))
                return true;
            if (EnumParseFunc.TryParse(type, out var raw))
            {
                parseFunc = raw;
                return true;
            }
            else
            {
                parseFunc = null;
                return false;
            }
        }

        #endregion
    }
}