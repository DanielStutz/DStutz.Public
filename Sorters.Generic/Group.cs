namespace DStutz.Sorters.Generic
{
    public interface IGroup<M>
        where M : notnull
    {
        public int MembersCount { get; }
        public ICollection<M> Members { get; }
        public void Add(M member);
        public string ToString(bool addGroup, bool addMembers);
        public string ToStringGroup();
        public string ToStringMembers();
        public string ToString(M member);
    }

    public abstract class Group<M>
        : IGroup<M>
        where M : notnull
    {
        #region Properties
        /***********************************************************/
        public abstract int MembersCount { get; }
        public abstract ICollection<M> Members { get; }
        public string? Error { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public virtual void Add(M member)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return ToString(true, true);
        }

        public string ToString(
            bool addGroup,
            bool addMembers)
        {
            if (addGroup && !addMembers)
                return ToStringGroup();

            if (!addGroup && addMembers)
                return ToStringMembers();

            return ToStringGroup() + " | " + ToStringMembers();
        }

        public virtual string ToStringGroup()
        {
            return GetType().Name;
        }

        public virtual string ToStringMembers()
        {
            return string.Join(
                ", ",
                Members.Select(e => ToString(e)));
        }

        public virtual string ToString(
            M member)
        {
            return member.ToString()!;
        }
        #endregion
    }
}
