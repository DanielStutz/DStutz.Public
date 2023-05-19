namespace DStutz.Data.Cruders
{
    public struct ValidationProblem
    {
        #region Properties
        /***********************************************************/
        public string Entity { get; set; }
        public string Property { get; set; }
        public string? Value { get; set; }
        public string Message { get; set; }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string ToString()
        {
            if (Value == null)
                return $"{Entity}.{Property} {Message}";

            return $"{Entity}.{Property} = {Value} {Message}";
        }
        #endregion
    }
}
