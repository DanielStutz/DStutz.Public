using DStutz.System.Extensions;

namespace DStutz.System.Unique
{
    public abstract class Assigner
    {
        private static Dictionary<char, int> Letters { get; } = new()
        {
            {'A', 10},
            {'Ä', 11}, {'À', 11},
            {'B', 12},
            {'C', 13},
            {'D', 14},
            {'E', 15},
            {'F', 16},
            {'G', 17},
            {'H', 18},
            {'I', 19},
            {'J', 20},
            {'K', 21},
            {'L', 22},
            {'M', 23},
            {'N', 24},
            {'O', 25},
            {'Ö', 26}, {'É', 26},
            {'P', 27},
            {'Q', 28},
            {'R', 29},
            {'S', 30},
            {'T', 31},
            {'U', 32},
            {'Ü', 33}, {'È', 33},
            {'V', 34},
            {'W', 35},
            {'X', 36},
            {'Y', 37},
            {'Z', 38},
        };

        private static int Assign2D(
            char character)
        {
            // Handle letters
            if (Letters.ContainsKey(character))
                return Letters[character];

            // Handle digits 0..9
            if (char.IsAsciiDigit(character))
                return character - 48;

            // All other chars
            return 0;
        }

        protected static long Assign2DxL(
            string word,
            int lenght)
        {
            word = word.Fix(lenght).ToUpper();

            long number = 0;
            long factor = 1;

            for (var i = lenght - 1; i >= 0; i--, factor *= 100)
                number += Assign2D(word[i]) * factor;

            return number;
        }
    }
}
