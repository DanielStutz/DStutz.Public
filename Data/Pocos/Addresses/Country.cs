using DStutz.System.Joiners;

namespace DStutz.Data.Pocos.Addresses
{
    public class Country : CodeName, IJoinable
    {
        #region Constructors
        /***********************************************************/
        public Country()
        { }

        public Country(
            string code,
            string name)
        {
            Code = code;
            Name = name;
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    (3, Code),
                    (40, Name)
                );
            }
        }
        #endregion
    }
}
