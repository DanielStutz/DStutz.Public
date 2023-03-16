namespace DStutz.Data
{
    public abstract class Abc
    {
        private static List<(char C, int N)> permutation =
            new List<(char, int)>()
        {
            (' ',  0),
            ('0',  0),
            ('1',  1),
            ('2',  2),
            ('3',  3),
            ('4',  4),
            ('5',  5),
            ('6',  6),
            ('7',  7),
            ('8',  8),
            ('9',  9),
            ('A', 10),
            ('Ä', 11),
            ('B', 12),
            ('C', 13),
            ('D', 14),
            ('E', 15),
            ('F', 16),
            ('G', 17),
            ('H', 18),
            ('I', 19),
            ('J', 20),
            ('K', 21),
            ('L', 22),
            ('M', 23),
            ('N', 24),
            ('O', 25),
            ('Ö', 26),
            ('P', 27),
            ('Q', 28),
            ('R', 29),
            ('S', 30),
            ('T', 31),
            ('U', 32),
            ('Ü', 33),
            ('V', 34),
            ('W', 35),
            ('X', 36),
            ('Y', 37),
            ('Z', 38),
        };

        // 3'838'383'838'383'800'000 (ZZZZZZZ)
        // 9'223'372'036'854'775'807 (Int64.Max)
        public static long GetPrimaryKey(
            string word)
        {
            // 7x2 digits for the first 7 characters of the word
            // 5x1 zero
            return GetNumber(word, 7) * 100000;
        }

        public static long GetNumber424(
            string word1,
            string word2)
        {
            // 4x2 digits for word1
            // 1x2 zeros
            // 4x2 digits for word2
            return GetNumber(word1, 4) * 10000000000 + GetNumber(word2, 4);
        }

        public static int GetNumber(
            char character)
        {
            foreach (var p in permutation)
                if (p.C == character)
                    return p.N;

            return 0;
        }

        public static long GetNumber(
            string word,
            int lenght)
        {
            word = GetWord(word, lenght);

            long number = 0;
            long factor = 1;

            for (var i = lenght - 1; i >= 0; i--, factor *= 100)
                number += GetNumber(word[i]) * factor;

            return number;
        }

        public static string GetWord(
            string word,
            int lenght)
        {
            if (word.Length >= lenght)
                return word.Substring(0, lenght).ToUpper();

            // Append spaces if the word is too short
            return string.Format("{0,-" + lenght + "}", word).ToUpper();
        }
    }
}
