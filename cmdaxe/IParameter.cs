using System;

namespace cmdaxe
{
    /// <summary>Represents a command-line parameter</summary>
    public interface IParameter
    {
        #region abstract properties

        /// <summary>Parameter name</summary>
        public string Name { get; }

        /// <summary>Parameter description</summary>
        public string Desc { get; }

        #endregion
        
        #region abstract methods

        /// <summary>Sets the value of the underlying field or property</summary>
        /// <param name="instance">Instance that contains the underlying field or property</param>
        /// <param name="value">Value</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="instance"/> is null
        /// </exception>
        public void SetValue(object instance, object value);

        #endregion
    }
}