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
        public virtual IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    ('L', 3, Type),
                    ('L', 2, Lang),
                    ('L', 80, Title),
                    ('L', 80, Href),
                    ('L', 20, Tags),
                    ('L', 20, Copyright)
                );
            }
        }
        #endregion
    }
}
