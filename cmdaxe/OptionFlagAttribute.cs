using System;

namespace cmdaxe
{
    /// <summary>
    ///     Specifies a command-line flag<br/>
    ///     The underlying field/property will be set to true if the flag is specified by the command-line user<br/>
    ///     That said, the underlying field/property must be a boolean
    /// </summary>
    /// <param name="name">Parameter name; this is also its keyword</param>
    /// <param name="shortcut">Shortcut character</param>
    /// <param name="desc">Parameter description</param>
    public class OptionFlagAttribute(
        string? name = null, char shortcut = '\0', string? desc = null) : 
        OptionAttribute(name, shortcut, desc)
    { }
}