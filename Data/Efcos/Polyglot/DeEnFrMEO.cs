using DStutz.System.Joiners;

using DStutz.Data.Pocos.Polyglot;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Polyglot
{
    public class DeEnFrMEO
        : IEfco<DeEnFrMPO>, IDeEnFr
    {
        #region Properties
        /***********************************************************/
        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return DeEnFrMapper.New.Joiner(this); }
        }

        public DeEnFrMPO Map()
        {
            return DeEnFrMapper.New.Map<DeEnFrMPO>(this);
        }
        #endregion
    }

    public class DeEnFrMapper
        : IMapper<IDeEnFr>
    {
        public static DeEnFrMapper New { get; } = new DeEnFrMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDeEnFr e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR)
            ).Add(data);
        }

        public E Map<E>(
            IDeEnFr e1) where E : IDeEnFr, new()
        {
            return new E()
            {
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
            };
        }
        #endregion
    }
}
