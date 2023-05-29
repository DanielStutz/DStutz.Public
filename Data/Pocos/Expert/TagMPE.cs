using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface ITag
        : IDeEnFrOLD, IPolyglotOLD
    {
        public long Pk1 { get; set; }
    }

    public class TagMPE
        : IPoco<ITag>, ITag
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return TagMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : ITag, new()
        {
            return TagMapper.New.Map<E>(this);
        }

        public string FindText(string ISOCode639)
        {
            return PolyglotText.Find(this, ISOCode639);
        }
        #endregion
    }
}
