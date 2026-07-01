using System.Collections.Immutable;
using System.IO;

namespace cmdaxe
{
    /// <summary>Represents a <see cref="cmdaxe"/> context</summary>
    public interface IContext
    {
        #region abstract properties

        /// <summary>Original context</summary>
        public IContext? Original { get; }

        /// <summary>Name of the entry assembly</summary>
        public string EntryName { get; }

        /// <summary>Command groups</summary>
        public ICmdGroups CommandGroups { get; }

        /// <summary>Parse functions</summary>
        public IParseFuncs ParseFuncs { get; }

        /// <summary>Syntax prefixes</summary>
        public ImmutableArray<string> Prefixes { get; }

        /// <summary>User raw input</summary>
        public ImmutableArray<string> RawInput { get; }

        #endregion

        #region internal methods

        internal string MM_GetPrefix()
        {
            using StringWriter w = new();
            foreach (var prefix in Prefixes)
            { w.Write(prefix); w.Write(' '); }
            return w.ToString();
        }

        #endregion
    }
}