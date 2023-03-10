using DStutz.System.Joiners;

using DStutz.Data.Pocos.Polyglot;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1.0
namespace DStutz.Data.Efcos.Polyglot
{
    public abstract class DeEnMEE
        : IEfco<DeEnMPE>, IDeEnKey
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

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

        public DeEnMPE Map()
        {
            return DeEnMapper.New.Map<DeEnMPE>(this);
        }
        #endregion
    }

    public class DeEnKeyMapper
        : IMapper<IDeEnKey>
    {
        public static DeEnKeyMapper New { get; } = new DeEnKeyMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDeEnKey e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN)
            ).Add(data);
        }

        public E Map<E>(
            IDeEnKey e1) where E : IDeEnKey, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                DE = e1.DE,
                EN = e1.EN,
            };
        }
        #endregion
    }
}
