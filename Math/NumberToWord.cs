using System.Text;

namespace DStutz.Math
{
    /*
     * Duden: Sollen Zahlen in Wörtern angegeben werden, schreibt man sie zusammen,
     * sofern sie unter einer Million liegen: 1965 = eintausendneunhundertfünfundsechzig
     * oder neunzehnhundertfünfundsechzig. Zahlen über einer Million schreibt man dagegen
     * getrennt: 2'120'419 = zwei Millionen einhundertzwanzigtausendvierhundertneunzehn.
     */
    public abstract class NumberToWord
    {
        #region Fields
        /***********************************************************/
        private static readonly string AND = "und";
        private static readonly bool ADD_AND_TO_HUNDREDS = true;
        private static readonly bool ADD_AND_TO_THOUSENDS = true;

        private static readonly string ZERO = "null";

        private static readonly string ONE = "eins";    // 1 = eins
        private static readonly string ONE2 = "eine";   // 1'000'000 = eine Million

        private static readonly string[] ONES = {
            "",
            "ein",  // 100 = einhundert, 1000 = eintausend
            "zwei",
            "drei",
            "vier",
            "fünf",
            "sechs",
            "sieben",
            "acht",
            "neun",
            "zehn",
            "elf",
            "zwölf",
            "dreizehn",
            "vierzehn",
            "fünfzehn",
            "sechzehn",
            "siebzehn",
            "achtzehn",
            "neunzehn"
        };

        private static readonly string[] TENS = {
            "",
            "zehn",
            "zwanzig",
            "dreissig",
            "vierzig",
            "fünfzig",
            "sechzig",
            "siebzig",
            "achtzig",
            "neunzig"
        };

        private static string HUNDRED = "hundert";
        private static string THOUSEND = "tausend";

        private static string MILLION = "Million";
        private static string MILLIONS = "Millionen";
        #endregion

        #region Methods wording
        /***********************************************************/
        public static string ToWords(double number)
        {
            return ToWords((int)number);
        }

        public static string ToWords(int number)
        {
            if (number == 0)
                return ZERO;

            //if (number == 1)
            //    return ONE;

            StringBuilder sb = new StringBuilder();
            //sb.Append(number + ": ");

            int[] groups = GetGroups(number);

            // Millions if any
            if (groups[0] > 0)
            {
                sb.Append(GetCyphersInWords(GetCyphers(groups[0]), ONE2) + " ");

                if (groups[0] == 1)
                {
                    sb.Append(MILLION);
                }
                else
                {
                    sb.Append(MILLIONS);
                }
            }

            sb.Append(" ");

            // Thousands if any
            if (groups[1] > 0)
                sb.Append(GetCyphersInWords(GetCyphers(groups[1]), ONES[1]) + THOUSEND);

            // Hundreds, tens and ones if any
            if (groups[2] > 0)
            {
                if (ADD_AND_TO_THOUSENDS && groups[1] > 0)
                    sb.Append(AND);

                sb.Append(GetCyphersInWords(GetCyphers(groups[2]), ONE));
            }

            return sb.ToString().Trim();
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private static int[] GetGroups(int number)
        {
            int[] groups = new int[3];

            groups[0] = number / 1000000;
            int rest = number % 1000000;

            groups[1] = rest / 1000;
            groups[2] = rest % 1000;

            return groups;
        }

        private static int[] GetCyphers(int group)
        {
            int[] cyphers = new int[3];

            cyphers[0] = group / 100;
            int rest = group % 100;

            cyphers[1] = rest / 10;
            cyphers[2] = rest % 10;

            return cyphers;
        }

        private static string GetCyphersInWords(int[] cyphers, string one)
        {
            StringBuilder sb = new StringBuilder();

            // Hundreds if any
            if (cyphers[0] > 0)
                sb.Append(ONES[cyphers[0]] + HUNDRED);

            int num = cyphers[1] * 10 + cyphers[2];

            // Tens and/or ones if any
            if (num > 0)
            {
                if (ADD_AND_TO_HUNDREDS && cyphers[0] > 0)
                    sb.Append(AND);

                if (num == 1)
                {
                    // 1
                    sb.Append(one);
                }
                else
                {
                    if (num < 20)
                    {
                        // 2 to 19
                        sb.Append(ONES[num]);
                    }
                    else
                    {
                        // 20 to 99
                        if (cyphers[2] == 0)
                        {
                            // 20, 30, 40 and so on
                            sb.Append(TENS[cyphers[1]]);
                        }
                        else
                        {
                            // 21 ... 29, 31 ... 39, 41 ... 49 and so on
                            sb.Append(ONES[cyphers[2]] + AND + TENS[cyphers[1]]);
                        }
                    }
                }
            }

            return sb.ToString();
        }
        #endregion

        #region Methods wording
        /***********************************************************/
        public static void Testing()
        {

            for (int i = 0; i <= 31; i++)
                Console.WriteLine(NumberToWord.ToWords(i));

            for (int t = 3; t <= 5; t++)
                for (int o = 0; o <= 9; o++)
                    Console.WriteLine(NumberToWord.ToWords(t * 10 + o));

            for (int h = 1; h <= 2; h++)
                for (int t = 0; t <= 3; t++)
                    for (int o = 0; o <= 2; o++)
                        Console.WriteLine(NumberToWord.ToWords(h * 100 + t * 10 + o));

            Console.WriteLine(NumberToWord.ToWords(999));
            Console.WriteLine(NumberToWord.ToWords(1000));
            Console.WriteLine(NumberToWord.ToWords(1001));
            Console.WriteLine(NumberToWord.ToWords(25000));
            Console.WriteLine(NumberToWord.ToWords(123456));
            Console.WriteLine(NumberToWord.ToWords(1234567));
            Console.WriteLine(NumberToWord.ToWords(2234567));
            Console.WriteLine(NumberToWord.ToWords(999234567));
        }
        #endregion
    }
}
