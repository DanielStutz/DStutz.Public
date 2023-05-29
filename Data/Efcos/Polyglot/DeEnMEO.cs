using DStutz.Data.Pocos.Polyglot;

using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Polyglot
{
    public class DeEnMEO
        : IEfco<DeEnMPO>, IDeEnOLD
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
        : IMapper<IDeEnOLD>
    {
        public static DeEnMapper New { get; } = new DeEnMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDeEnOLD e1,
            params IJoinableOld?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN)
            ).Add(data);
        }

        public E Map<E>(
            IDeEnOLD e1) where E : IDeEnOLD, new()
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
