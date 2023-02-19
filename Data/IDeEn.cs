namespace DStutz.Data
{
    public interface IDeEn
    {
        public string? DE { get; set; }
        public string? EN { get; set; }
    }

    public interface IDeEnKey : IDeEn
    {
        public long Pk1 { get; set; }
    }
}
