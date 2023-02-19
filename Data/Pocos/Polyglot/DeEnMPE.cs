using DStutz.System.Joiners;

using DStutz.Data.Efcos.Polyglot;

// Version 1.1
namespace DStutz.Data.Pocos.Polyglot
{
    public class DeEnMPE
        : IPoco<IDeEnKey>, IDeEnKey, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
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
            return DeEnKeyMapper.New.Joiner(this);
        }

        public E Map<E>() where E : IDeEnKey, new()
        {
            return DeEnKeyMapper.New.Map<E>(this);
        }
        #endregion
    }
}
