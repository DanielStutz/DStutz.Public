using System.Collections.Generic;

namespace DStutz.Sorters.Generic
{
    /// <summary>
    /// A <c>GroupDictionary</c> holds non distinct <c>Members</c>.
    /// </summary>
    public abstract class GroupList<M>
        : Group<M>
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public IList<M> List { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected GroupList()
        {
            List = new List<M>();
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override int MembersCount { get { return List.Count; } }
        public override ICollection<M> Members { get { return List; } }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public override void Add(M member)
        {
            List.Add(member);
        }
        #endregion
    }
}
