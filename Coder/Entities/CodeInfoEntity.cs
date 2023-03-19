namespace DStutz.Coder.Entities
{
    public class CodeInfoEntity : CodeInfo
    {
        public bool Asymmetric { get; set; } = false;
        public string? ImportJsonFrom { get; set; } // Used by OrderPu and OrderSa
    }
}
