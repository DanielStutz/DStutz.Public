namespace DStutz.Data
{
    public interface IRel
        : IJoinableOld
    {
        public long OwnerPk1 { get; set; }
        public int OrderBy { get; set; }
        public long RelatedPk1 { get; set; }
    }

    public interface IRelAny<E> : IRel
    {
        public E? Related { get; set; }
    }

    public interface IRelNum<E> : IRel
    {
        public E? Related { get; set; }
        public int Number { get; set; }
    }

    public interface IRelType<E> : IRel
    {
        public E? Related { get; set; }
        public string? Type { get; set; }
    }
}
