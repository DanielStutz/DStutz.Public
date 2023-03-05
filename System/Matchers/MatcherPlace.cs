using System.Text.RegularExpressions;

namespace DStutz.System.Matchers
{
    public interface IMatcherNamed
    {
        public bool Matches(string name);
    }

    public class MatcherPlace : IMatcherNamed
    {
        // Regex for a single or multiple white spaces
        private static Regex regex = new Regex("[ ]{1,}", RegexOptions.None);

        // The unsafe place name is provided e.g. by a customer
        private readonly string UnsafePlaceName;

        public MatcherPlace(string unsafePlaceName)
        {
            // The unsafe place name may contain multiple white spaces
            // and / or may have incorrect lower and upper case letters
            UnsafePlaceName = regex.Replace(unsafePlaceName, "").ToLower();
        }

        public bool Matches(string correctPlaceName)
        {
            // The safe place name is provided by a database
            var safePlaceName = regex.Replace(correctPlaceName, "").ToLower();

            return
                UnsafePlaceName.Equals(safePlaceName) ||
                //UnsafePlaceName.StartsWith(safePlaceName) ||
                safePlaceName.StartsWith(UnsafePlaceName);
        }
    }
}
