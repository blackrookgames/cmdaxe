using System;

namespace cmdaxe
{
    /// <summary>Specifies a command-line parameter</summary>
    /// <param name="name">Parameter name</param>
    /// <param name="desc">Parameter description</param>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public abstract class ParameterAttribute(string name, string desc) : Attribute
    {
        #region fields

        private readonly string f_Name = name;
        private readonly string f_Desc = desc;

        #endregion

        #region properties

        /// <summary>Parameter name</summary>
        public string Name => f_Name;

        /// <summary>Parameter description</summary>
        public string Desc => f_Desc;

        #endregion
    }
}