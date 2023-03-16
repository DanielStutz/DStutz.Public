namespace DStutz.Data.Pocos
{
    public class LinkWithImage
        : Link
    {
        #region Properties
        /***********************************************************/
        public string HrefImage { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public LinkWithImage(
            string type)
            : base(type)
        { }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (3, Type),
                    (2, Lang),
                    (80, Title),
                    (80, Href),
                    (20, Tags),
                    (20, Copyright),
                    (80, HrefImage)
                );
            }
        }
        #endregion
    }
}
