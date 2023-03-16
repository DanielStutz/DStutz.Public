using DStutz.Data.Efcos.Polyglot;
using DStutz.Data.Pocos;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos
{
    [Table("zombie")]
    public class ZombieMEE
        : IEfco<ZombieMPE>, IZombie
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("order_by"), Key]
        public int OrderBy { get; set; }

        [Column("prename")]
        public string Prename { get; set; }

        [Column("surname")]
        public string Surname { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public DeEnMEO Polyglot { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ZombieMapper.New.Joiner(this, Polyglot); }
        }

        public ZombieMPE Map()
        {
            return ZombieMapper.New.Map<ZombieMPE>(this);
        }
        #endregion
    }

    public class ZombieMapper
        : IMapper<IZombie>
    {
        public static ZombieMapper New { get; } = new ZombieMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IZombie e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('R', 3, e1.OrderBy),
                ('L', 20, e1.Prename),
                ('L', 20, e1.Surname)
            ).Add(data);
        }

        public E Map<E>(
            IZombie e1) where E : IZombie, new()
        {
            var e2 = new E()
            {
                Pk1 = e1.Pk1,
                OrderBy = e1.OrderBy,
                Prename = e1.Prename,
                Surname = e1.Surname,
            };

            if (typeof(E) == typeof(ZombieMEE))
            {
                ZombieMPE poco = (ZombieMPE)(object)e1;
                ZombieMEE efco = (ZombieMEE)(object)e2;

                efco.Polyglot =
                    Mapper.MapMandatory(
                        poco.Polyglot,
                        e => e.Map<DeEnMEO>());
            }
            else if (typeof(E) == typeof(ZombieMPE))
            {
                ZombieMEE efco = (ZombieMEE)(object)e1;
                ZombieMPE poco = (ZombieMPE)(object)e2;

                poco.Polyglot =
                    Mapper.MapMandatory(
                        efco.Polyglot,
                        e => e.Map());
            }
            else
            {
                throw new NotImplementedException();
            }

            return e2;
        }
        #endregion
    }
}
