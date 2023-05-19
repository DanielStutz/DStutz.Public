using static DStutz.System.Stringers.Reader;
using static DStutz.System.Stringers.Spliter;

namespace DStutz.Apps.Services.Food.Files
{
    public class FoodItemCSV
        : IJoinable
    {
        #region Fields
        /***********************************************************/
        private static (string, string)[] ReferenceUnits =
            new (string, string)[]
            {
                ("pro 100g essbarer Anteil", "100g"),
                ("pro 100 ml", "100ml"),
            };
        #endregion

        #region Properties
        /***********************************************************/
        public long Pk1 { get; }
        public int? IdV40 { get; }
        public int? IdSwissFIR { get; }
        public string Name { get; }
        public string? Synonyms { get; }
        public IEnumerable<string> Categories { get; }
        public string? Density { get; }
        public double Energy1 { get; set; } // kJ
        public double Energy2 { get; set; } // kcal
        public string ReferenceUnit { get; }
        public bool ChangedEntry { get; }
        public Dictionary<int, NutrientDataCSV> Nutrients { get; } = new();
        #endregion

        #region Properties additional
        /***********************************************************/
        public IEnumerable<string> CategoriesSorted
        {
            get
            {
                var sorted = new SortedSet<string>();

                foreach (var category in Categories)
                    sorted.Add(category);

                return sorted.ToList();
            }
        }
        #endregion

        #region Constructors
        /***********************************************************/
        public FoodItemCSV(
            string[] cells)
        {
            try
            {
                Pk1 = long.Parse(cells[0]);

                if (!string.IsNullOrWhiteSpace(cells[1]))
                    IdV40 = int.Parse(cells[1]);

                if (!string.IsNullOrWhiteSpace(cells[2]))
                    IdSwissFIR = int.Parse(cells[2]);

                Name = ReadOrThrow(cells[3]);

                if (!string.IsNullOrWhiteSpace(cells[4]))
                {
                    var s1 = ReadOrThrow(cells[4], "\"");
                    var s2 = SplitOrThrow(s1, ';');
                    Synonyms = string.Join("; ", s2);
                }

                if (!string.IsNullOrWhiteSpace(cells[5]))
                {
                    var c1 = ReadOrThrow(cells[5], "\"");
                    var c2 = SplitOrThrow(c1, ';', '/')!;
                    Categories = c2.Select(c => string.Join(" / ", c));
                }

                Density = Read(cells[6]);
                ReferenceUnit = ReadOrThrow(cells[7], ReferenceUnits);
                Energy1 = double.Parse(ReadOrThrow(cells[8]));
                Energy2 = double.Parse(ReadOrThrow(cells[11]));

                //var d = Math.Abs(Energy1 - Energy2 / 0.239);
                //var p = d / Energy1;

                //if (p > 0.5)
                //    Console.WriteLine(p.ToString("N4"));

                var yesOrNo = ReadOrThrow(cells[128]).ToLower();

                if (yesOrNo.Equals("ja"))
                    ChangedEntry = true;
                else if (yesOrNo.Equals("nein"))
                    ChangedEntry = false;
                else
                    throw new Exception($"Unknown value '{yesOrNo}'");

                // There are 38 entries, see table 'nutrient' in database
                // and columns in file *.csv. The order of entries must match!
                for (int number = 1, i = 14; number <= 38; number++)
                    Nutrients.Add(
                        number,
                        new NutrientDataCSV(
                            number,
                            cells[i++],
                            cells[i++],
                            cells[i++]
                        )
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Unable to read item '{cells[3]}'", ex);
            }
        }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get
            {
                var nutrients = string.Join(
                    " | ",
                    Nutrients.Values.Select(e => e.Joiner.Row));

                return new Joiner(
                    ('R', 5, Pk1),
                    ('R', 4, IdV40),
                    ('R', 7, IdSwissFIR),
                    ('L', 30, Name),
                    ('R', 4, Density),
                    ('L', 5, ReferenceUnit),
                    ('R', 5, ChangedEntry),
                    ('L', nutrients.Length, nutrients)
                );
            }
        }
        #endregion
    }
}
