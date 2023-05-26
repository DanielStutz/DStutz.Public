using DStutz.Data.Efcos.Expert.Notes;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert.Notes
{
    public interface INote
    {
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long AuthorPk1 { get; set; }
    }

    public class NoteMPE
        : IPoco<INote>, INote
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Lang { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long AuthorPk1 { get; set; }
        public AuthorMPE Author { get; set; }
        #endregion

        #region Properties and methods implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get { return NoteMapper.New.Joiner(this); }
        }

        public E Map<E>() where E : INote, new()
        {
            return NoteMapper.New.Map<E>(this);
        }
        #endregion
    }
}
