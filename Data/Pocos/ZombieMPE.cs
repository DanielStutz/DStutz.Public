using DStutz.System.Joiners;

using DStutz.Data.Efcos;
using DStutz.Data.Pocos.Polyglot;

// Version 1.1.0
namespace DStutz.Data.Pocos
{
    public interface IZombie
        : IOrdered
    {
        public long Pk1 { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
    }

    public class ZombieMPE
        : IPoco<IZombie>, IZombie
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public int OrderBy { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
        #endregion

        #region Properties owned
        /***********************************************************/
        public DeEnMPO Polyglot { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return ZombieMapper.New.Joiner(this, Polyglot); }
        }

        public E Map<E>() where E : IZombie, new()
        {
            return ZombieMapper.New.Map<E>(this);
        }
        #endregion
    }
}
