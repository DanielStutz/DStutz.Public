namespace DStutz.Data.Efcos.Food
{
    public interface INutrientValue
    {
        public int OrderBy { get; set; }
        public string Value { get; set; }
        public string? Derivation { get; set; }
        public string? Sources { get; set; }
    }
}
