namespace DStutz.Data.Pocos
{
    public abstract class CodeName
    {
        #region Properties
        /***********************************************************/
        public string Code { get; set; }
        public string Name { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected CodeName()
        { }
        #endregion

        #region Methods
        /***********************************************************/
        public string GetCodeName()
        {
            return $"{Code} {Name}";
        }

        public string GetNameCode()
        {
            return $"{Name} {Code}";
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            return $"{Code} {Name}";
        }
        #endregion
    }
}
