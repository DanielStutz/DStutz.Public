namespace DStutz.Data.Pocos
{
    public abstract class RelPE
        : IRel
    {
        public long OwnerPk1 { get; set; }
        public int OrderBy { get; set; }
        public long RelatedPk1 { get; set; }

        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (20, OwnerPk1),
                    (2, OrderBy),
                    (20, RelatedPk1)
                );
            }
        }
    }

    public class RelPEAny<P, I>
        : RelPE
        where P : I, IPoco<I>, new()
    {
        public P? Related { get; set; }

        public R Map<R, E>()
            where R : IRelAny<E>, new()
            where E : I, new()
        {
            var efco = new R()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
            };

            if (Related != null)
                efco.Related = Related.Map<E>();

            return efco;
        }
    }

    public class RelPENum<P, I>
        : RelPE
        where P : I, IPoco<I>, new()
    {
        public P? Related { get; set; }
        public int Number { get; set; }

        public R Map<R, E>()
            where R : IRelNum<E>, new()
            where E : I, new()
        {
            var efco = new R()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
                Number = Number,
            };

            if (Related != null)
                efco.Related = Related.Map<E>();

            return efco;
        }
    }

    public class RelPEType<P, I>
        : RelPE
        where P : I, IPoco<I>, new()
    {
        public P? Related { get; set; }
        public string? Type { get; set; }

        public R Map<R, E>()
            where R : IRelType<E>, new()
            where E : I, new()
        {
            var efco = new R()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
                Type = Type,
            };

            if (Related != null)
                efco.Related = Related.Map<E>();

            return efco;
        }
    }
}
