using System;

namespace cmdaxe
{
    /// <summary>
    ///     <para>
    ///         Specifies a method a parse function
    ///     </para>
    ///     <para>
    ///         In order for the method to be considered a parse function, it must
    ///         <list type="bullet">
    ///             <item>Be within a top-level class</item>
    ///             <item>Be static</item>
    ///             <item>Be assignable to <see cref="ParseFunc"/></item>
    ///         </list>
    ///     </para>
    /// </summary>
    /// <param name="type">Target type</param>
    /// <param name="displayName">Display name for target type (ex: "8-bit unsigned integer")</param>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ParseFuncAttribute(Type type, string? displayName = null) : Attribute
    {
        #region fields

        private readonly Type f_Type = type;
        private readonly string? f_DisplayName = displayName;

        #endregion

        #region properties

        /// <summary>Target type</summary>
        public Type Type => f_Type;

        /// <summary>Display name for target type (ex: "8-bit unsigned integer")</summary>
        public string? DisplayName => f_DisplayName;

        #endregion
    }
}