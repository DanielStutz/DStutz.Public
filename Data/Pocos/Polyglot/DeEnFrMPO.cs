using DStutz.System.Joiners;

using DStutz.Data.Efcos.Polyglot;

// Version 1.1.0
namespace DStutz.Data.Pocos.Polyglot
{
    public class DeEnFrMPO
        : IPoco<IDeEnFr>, IDeEnFr, IPolyglot
    {
        #region Properties
        /***********************************************************/
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        #endregion

        #region Asymmetric code
        /***********************************************************/
        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return DeEnFrMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : IDeEnFr, new()
        {
            return DeEnFrMapper.New.Map<E>(this);
        }
        #endregion
    }
}
