using DStutz.Data;
using DStutz.System.Joiners;

namespace DStutz.Collections.Generic.Collectors
{
    public class MList<M> : MCollection<M>
        where M : IJoinable
    {
        #region Properties
        /***********************************************************/
        public List<M> List { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public MList()
        {
            List = new List<M>();
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override int Count
        {
            get { return List.Count; }
        }

        public override ICollection<M> Members
        {
            get { return List; }
        }
        #endregion

        #region Methods members implementing
        /***********************************************************/
        public override void Add(M member)
        {
            List.Add(member);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override void Write()
        {
            foreach (var member in List)
                member.Joiner().WriteRow();
        }
        #endregion
    }
}
