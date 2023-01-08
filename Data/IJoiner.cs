namespace DStutz.Data
{
    public interface IJoiner
    {
        public void WriteCol();
        public void WriteRow();
        public string Col();
        public string Row();
        public string RowNoWhitespaces();
    }
}
