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
        public override IJoiner Joiner
        {
            get
            {
                return base.Joiner.Add(
                    ('L', 80, HrefImage)
                );
            }
        }
        #endregion
    }
}
