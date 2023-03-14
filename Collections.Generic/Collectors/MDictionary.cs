using DStutz.Data;
using DStutz.System.Joiners;

namespace DStutz.Collections.Generic.Collectors
{
    public class MDictionary<M> : MCollection<M>
        where M : IJoinable, ITyped
    {
        #region Properties
        /***********************************************************/
        public IDictionary<string, List<M>> Dictionary { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public MDictionary()
        {
            Dictionary = new SortedDictionary<string, List<M>>();
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public override int Count
        {
            get
            {
                int count = 0;

                foreach (var list in Dictionary.Values)
                    count += list.Count;

                return count;
            }
        }

        public override ICollection<M> Members
        {
            get
            {
                List<M> members = new List<M>();

                foreach (var list in Dictionary.Values)
                    members.AddRange(list);

                return members;
            }
        }
        #endregion

        #region Methods members implementing
        /***********************************************************/
        public override void Add(M member)
        {
            Add(member.Type, member);
        }

        public void Add(int type, M member)
        {
            Add(type.ToString(), member);
        }

        public void Add(string type, M member)
        {
            if (!Dictionary.ContainsKey(type))
                Dictionary.Add(type, new List<M>());

            Dictionary[type].Add(member);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override void Write()
        {
            foreach (var members in Dictionary.Values)
                foreach (var member in members)
                    member.Joiner.WriteRow();
        }
        #endregion
    }
}
