using DStutz.System.Joiners;

using DStutz.Data.Efcos.Polyglot;

// Version 1.1
namespace DStutz.Data.Pocos.Polyglot
{
    public class DeEnMPO
        : IPoco<IDeEn>, IDeEn, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public string? DE { get; set; }
        public string? EN { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return DeEnMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IDeEn, new()
        {
            return DeEnMapper.New.Map<E>(this);
        }
        #endregion
    }
}
