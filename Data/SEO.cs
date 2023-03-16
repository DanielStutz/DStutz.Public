using System.Globalization;

namespace DStutz.Data
{
    public abstract class SEO
    {
        #region Properties
        /***********************************************************/
        public string ISOCode6391 { get; }
        public string Title { get; set; }
        public string TitleLong { get; set; }
        public string Handle { get; set; }
        public string Description { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public SEO(
            CultureInfo ci)
        {
            ISOCode6391 = ci.TwoLetterISOLanguageName;
        }
        #endregion
    }
}
