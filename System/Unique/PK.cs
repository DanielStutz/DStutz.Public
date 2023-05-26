namespace DStutz.System.Unique
{
    public abstract class PK : Assigner
    {
        /// <summary>
        ///     Assign w to 3x2 digits
        /// </summary>
        /// <returns>
        ///     A number with 6 digits
        /// </returns>
        public static long Assign6D(
            string w)
        {
            return Assign2DxL(w, 3);
        }

        /// <summary>
        ///     Assign w to 7x2 digits and 5 zeros
        /// </summary>
        /// <returns>
        ///     A number with 19 digits
        /// </returns>
        public static long Assign14D5Z(
            string w)
        {
            // 3'838'383'838'383'800'000 (ZZZZZZZ)
            // 9'223'372'036'854'775'807 (Int64.Max)
            return Assign2DxL(w, 7) * CMath.E5;
        }

        /// <summary>
        ///     Assign w1 and w2 to 4x2 digits, 2 zeros and 4x2 digits
        /// </summary>
        /// <returns>
        ///     A number with 18 digits
        /// </returns>
        public static long Assign8D2Z8D(
            string w1,
            string w2)
        {
            return Assign2DxL(w1, 4) * CMath.E10 + Assign2DxL(w2, 4);
        }
    }
}
