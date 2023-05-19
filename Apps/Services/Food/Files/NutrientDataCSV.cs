using DStutz.Data.Efcos.Food;
using static DStutz.System.Stringers.Reader;
using static DStutz.System.Stringers.SpliterParser;

namespace DStutz.Apps.Services.Food.Files
{
    public class NutrientDataCSV
        : INutrientValue, IJoinable
    {
        #region Fields
        /***********************************************************/
        private static (string, string)[] Derivations =
            new (string, string)[]
            {
                ("-", "-"),
                ("Automatisierte Berechnung", "CA"),
                ("Berechnung anhand von Zutaten", "CI"),
                ("Schätzung", "ES"),
            };
        #endregion

        #region Properties
        /***********************************************************/
        public int OrderBy { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
        public IEnumerable<long>? SourcesParsed { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public NutrientDataCSV(
            int orderBy,
            string value,
            string? derivation,
            string? sources)
        {
            try
            {
                OrderBy = orderBy;
                Value = ReadOrThrow(value);
                Derivation = Read(derivation, Derivations);
                SourcesParsed = Split<long>(sources, ';', "\"");

                if (SourcesParsed != null)
                    Sources = string.Join(",", SourcesParsed);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Unable to read entry # {orderBy}", ex);
            }
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                return new Joiner(
                    " ",
                    ('R', 2, OrderBy),
                    ('R', 5, Value),
                    ('R', 2, Derivation),
                    ('R', 8, Sources)
                );
            }
        }
        #endregion
    }
}
