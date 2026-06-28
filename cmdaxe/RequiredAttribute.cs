using System;

namespace cmdaxe
{
    /// <summary>
    ///     <para>
    ///         Specifies a command-line parameter that must be given an argument by the user
    ///     </para>
    ///     <para>
    ///         Fields take order priority over properties.
    ///     </para>
    ///     <para>
    ///         Arrays are also supported as an underlying field/property, 
    ///         but only one is supported per command and it will always be parsed last.
    ///     </para>
    /// </summary>
    /// <param name="name">Parameter name</param>
    /// <param name="desc">Parameter description</param>
    public class RequiredAttribute(
        string name = null, string desc = null) : 
        ParameterAttribute(name, desc)
    { }
}