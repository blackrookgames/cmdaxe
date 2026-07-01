using System;
using System.Collections.Generic;

namespace cmdaxe
{
    /// <summary>Represents a collection of command groups</summary>
    public interface ICmdGroups : IEnumerable<ICmdGroup>
    {
        #region abstract properties

        /// <summary><see cref="cmdaxe"/> context</summary>
        public IContext Context { get; }

        /// <summary>Number of groups</summary>
        public int Count { get; }

        #endregion

        #region abstract methods

        /// <summary>Attempts to find the group with the specified name</summary>
        /// <param name="name">Group name</param>
        /// <param name="group">Found group</param>
        /// <returns>Whether or not successful</returns>
        public bool TryGet(string? name, out ICmdGroup? group);

        #endregion
    }
}