using DStutz.System.Joiners;

using DStutz.Data.Pocos.Polyglot;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version 1.1
namespace DStutz.Data.Efcos.Polyglot
{
    public abstract class DeEnFrMEE
        : IEfco<DeEnFrMPE>, IDeEnFrKey
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public long Pk1 { get; set; }

        [Column("de")]
        public string? DE { get; set; }

        [Column("en")]
        public string? EN { get; set; }

        [Column("fr")]
        public string? FR { get; set; }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return DeEnFrKeyMapper.New.Joiner(this);
        }

        public DeEnFrMPE Map()
        {
            return DeEnFrKeyMapper.New.Map<DeEnFrMPE>(this);
        }
        #endregion
    }

    public class DeEnFrKeyMapper
        : IMapper<IDeEnFrKey>
    {
        public static DeEnFrKeyMapper New { get; } = new DeEnFrKeyMapper();

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner(
            IDeEnFrKey e1,
            params IJoinable?[] data)
        {
            return new Joiner(
                //('L', 20, e1.GetType().Name),
                ('R', 20, e1.Pk1),
                ('L', 20, e1.DE),
                ('L', 20, e1.EN),
                ('L', 20, e1.FR)
            ).Add(data);
        }

        public E Map<E>(
            IDeEnFrKey e1) where E : IDeEnFrKey, new()
        {
            return new E()
            {
                Pk1 = e1.Pk1,
                DE = e1.DE,
                EN = e1.EN,
                FR = e1.FR,
            };
        }
        #endregion
    }
}
