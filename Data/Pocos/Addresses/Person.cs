using DStutz.Data.Pocos.People;

namespace DStutz.Data.Pocos.Addresses
{
    public class Person
    {
        #region Properties
        /***********************************************************/
        public Gender Gender { get; set; }
        public string Prename { get; set; }
        public string Surname { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string GetPreSurName()
        {
            return $"{Prename} {Surname}";
        }

        public string GetSurPreName()
        {
            return $"{Surname} {Prename}";
        }

        public string GetGenderSurPreName()
        {
            return $"{Gender.Abbr} {Surname} {Prename}";
        }

        public string GetPreSurNameShort()
        {
            return Prename[..1] + ". " + Surname;
        }

        public string GetSurPreNameShort()
        {
            return Surname + " " + Prename[..1] + ".";
        }
        #endregion
    }
}
