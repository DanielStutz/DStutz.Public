using System;
using System.Collections.Generic;

namespace DStutz.Sorters.Generic
{
    /// <summary>
    /// A <c>GroupDictionary</c> holds distinct <c>Members</c>.
    /// </summary>
    public abstract class GroupDictionary<K, M>
        : Group<M>
        where K : notnull
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public IDictionary<K, M> Dictionary { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected GroupDictionary()
            : this(null)
        { }

        protected GroupDictionary(
            SortOrder order)
            : this(new SortComparer<K>(order))
        { }

        protected GroupDictionary(
            IComparer<K>? comparer)
        {
            Dictionary = new SortedDictionary<K, M>(comparer);
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override int MembersCount { get { return Dictionary.Count; } }
        public override ICollection<M> Members { get { return Dictionary.Values; } }
        #endregion

        #region Methods implementing
        /***********************************************************/
        protected void Add(K key, M member)
        {
            if (Dictionary.ContainsKey(key))
                throw new Exception($"Duplicate key {key}");

            Dictionary.Add(key, member);
        }
        #endregion
    }
}
