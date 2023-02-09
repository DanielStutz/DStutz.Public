namespace DStutz.System.Joiners
{
    public interface IJoiner
    {
        public string Col();
        public string Row();
        public string RowShort();
        public void WriteCol();
        public void WriteRow();
        public void WriteRowShort();
    }
}
