using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Place : CodeName, IJoinable
    {
        #region Properties
        /***********************************************************/
        public string? Language { get; set; }
        public string? RegionCode { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Place()
        { }

        public Place(
            string code,
            string name)
        {
            Code = code;
            Name = name;
        }

        public Place(
            string code,
            string name,
            string language,
            string regionCode)
        {
            Code = code;
            Name = name;
            Language = language;
            RegionCode = regionCode;
        }
        #endregion

        #region Methods implementing
        /***********************************************************/
        public IJoiner Joiner()
        {
            return new Joiner(
                (20, Code),
                (40, Name),
                (6, RegionCode)
            );
        }
        #endregion
    }
}
