namespace DStutz.Data.Pocos
{
    public class Link
        : IJoinable, ITyped
    {
        #region Properties
        /***********************************************************/
        public string Type { get; set; }
        public string? Lang { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string? Tags { get; set; }
        public string? Copyright { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Link(
            string type)
        {
            Type = type;
        }
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
                    (20, Copyright)
                );
            }
        }
        #endregion
    }
}
