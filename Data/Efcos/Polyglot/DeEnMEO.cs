using DStutz.Data.Pocos.Polyglot;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Polyglot
{
    public class DeEnMEO
        : IEfco<DeEnMPO>, IDeEn
    {
        #region Properties
        /***********************************************************/
        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return DeEnMapper.New.Joiner(this); }
        }

        public DeEnMPO Map()
        {
            return DeEnMapper.New.Map<DeEnMPO>(this);
        }
        #endregion
    }

    public class DeEnMapper
        : IMapper<IDeEn>
    {
        public static DeEnMapper New { get; } = new DeEnMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDeEn e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN)
            ).Add(data);
        }

        public E Map<E>(
            IDeEn e1) where E : IDeEn, new()
        {
            return new E()
            {
                DE = e1.DE,
                EN = e1.EN,
            };
        }
        #endregion
    }
}
