namespace DStutz.System.Joiners
{
    public interface IJoiner
    {
        public string Col();
        public string Row();
        public string RowNoWhitespaces();
        public void WriteCol();
        public void WriteRow();
    }
}
