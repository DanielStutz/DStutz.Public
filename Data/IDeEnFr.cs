namespace DStutz.Data
{
    public interface IDeEnFr
    {
        public string? DE { get; set; }
        public string? EN { get; set; }
        public string? FR { get; set; }
    }

    public interface IDeEnFrKey : IDeEnFr
    {
        public long Pk1 { get; set; }
    }
}
