using System;
using System.Collections.Immutable;
using System.Reflection;

namespace cmdaxe
{
    internal readonly struct InternalContext : IContext
    {
        #region init

        /// <summary>Initializer for <see cref="InternalContext"/></summary>
        /// <param name="original">Original context</param>
        /// <param name="prefixes">Prefixes</param>
        /// <param name="rawInput">Raw input</param>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="original"/> is null
        /// </exception>
        public InternalContext(IContext original, ImmutableArray<string> prefixes, ImmutableArray<string> rawInput)
        {
            ArgumentNullException.ThrowIfNull(original);
            f_Original = original;
            f_EntryName = original.EntryName;
            f_CommandGroups = original.CommandGroups;
            f_ParseFuncs = original.ParseFuncs;
            f_Prefixes = prefixes;
            f_RawInput = rawInput;
        }

        #endregion
        
        #region fields

        private readonly IContext f_Original;
        private readonly string f_EntryName;
        private readonly ICmdGroups f_CommandGroups;
        private readonly IParseFuncs f_ParseFuncs;
        private readonly ImmutableArray<string> f_Prefixes;
        private readonly ImmutableArray<string> f_RawInput;

        #endregion

        #region properties

        /// <inheritdoc/>
        public IContext? Original => f_Original;

        /// <inheritdoc/>
        public string EntryName => f_EntryName;

        /// <inheritdoc/>
        public ICmdGroups CommandGroups => f_CommandGroups;

        /// <inheritdoc/>
        public IParseFuncs ParseFuncs => f_ParseFuncs;

        /// <inheritdoc/>
        public ImmutableArray<string> Prefixes => f_Prefixes;

        /// <inheritdoc/>
        public ImmutableArray<string> RawInput => f_RawInput;

        #endregion
    }
}