using System;

namespace cmdaxe
{
    /// <summary>Represents information about a parse function</summary>
    public interface IParseFuncInfo
    {
        #region abstract properties

        /// <summary>Target type</summary>
        public Type Type { get; }

        /// <summary>Display name for target type (ex: "8-bit unsigned integer")</summary>
        public string? DisplayName { get; }
        
        /// <summary>Parse function</summary>
        public ParseFunc Func { get; }

        #endregion
    }
}