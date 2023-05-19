using DStutz.Data.Pocos;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DStutz.Data.Efcos
{
    public abstract class RelEE<O>
        : IRel, IOwned<O>
    {
        [Column("owner_pk1"), Key]
        public long OwnerPk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("related_pk1")]
        public long RelatedPk1 { get; set; }

        [ForeignKey("OwnerPk1")]
        public O? Owner { get; set; }

        public virtual IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    ('L', 20, OwnerPk1),
                    ('R', 3, OrderBy),
                    ('L', 20, RelatedPk1)
                );
            }
        }
    }

    public abstract class RelEEAny<O, E, P, I>
        : RelEE<O>, IRelAny<E>
        where E : I, IEfco<P>, new()
        where P : I, IPoco<I>, new()
    {
        [ForeignKey("RelatedPk1")]
        public E? Related { get; set; }

        public RelPEAny<P, I> Map()
        {
            var poco = new RelPEAny<P, I>()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
            };

            if (Related != null)
                poco.Related = Related.Map();

            return poco;
        }
    }

    public abstract class RelEENum<O, E, P, I>
        : RelEE<O>, IRelNum<E>
        where E : I, IEfco<P>, new()
        where P : I, IPoco<I>, new()
    {
        [ForeignKey("RelatedPk1")]
        public E? Related { get; set; }

        [Column("number")]
        public int Number { get; set; }

        public RelPENum<P, I> Map()
        {
            var poco = new RelPENum<P, I>()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
                Number = Number,
            };

            if (Related != null)
                poco.Related = Related.Map();

            return poco;
        }
    }

    public abstract class RelEEType<O, E, P, I>
        : RelEE<O>, IRelType<E>
        where E : I, IEfco<P>, new()
        where P : I, IPoco<I>, new()
    {
        [ForeignKey("RelatedPk1")]
        public E? Related { get; set; }

        [Column("type")]
        public string? Type { get; set; }

        public RelPEType<P, I> Map()
        {
            var poco = new RelPEType<P, I>()
            {
                OwnerPk1 = OwnerPk1,
                OrderBy = OrderBy,
                RelatedPk1 = RelatedPk1,
                Type = Type,
            };

            if (Related != null)
                poco.Related = Related.Map();

            return poco;
        }
    }
}
