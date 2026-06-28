using System;

namespace cmdaxe
{
    /// <summary>
    ///     <para>Specifies a class as a command</para>
    ///     <para>NOTE: In order for the class to be considered a valid command, it must contain a parameterless constructor</para>
    /// </summary>
    /// <param name="name">Command name</param>
    /// <param name="group">Group command belongs to</param>
    /// <param name="desc">Command description</param>
    /// <param name="helpKeyword">Keyword for displaying help</param>
    /// <param name="helpShort">Shortcut for displaying help</param>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute(
        string name = null, 
        string group = null,
        string desc = null,
        string helpKeyword = "help",
        char helpShort = 'h') : Attribute
    {
        #region fields

        private readonly string f_Name = name;
        private readonly string f_Group = group;
        private readonly string f_Desc = desc;
        private readonly string f_HelpKeyword = helpKeyword;
        private readonly char f_HelpShort = helpShort;

        #endregion

        #region properties

        /// <summary>Command name</summary>
        public string Name => f_Name;

        /// <summary>Group command belongs to</summary>
        public string Group => f_Group;

        /// <summary>Command description</summary>
        public string Desc => f_Desc;
        
        /// <summary>Keyword for displaying help</summary>
        public string HelpKeyword => f_HelpKeyword;

        /// <summary>Shortcut for displaying help</summary>
        public char HelpShort => f_HelpShort;

        #endregion
    }
}