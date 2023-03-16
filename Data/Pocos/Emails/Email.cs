namespace DStutz.Data.Pocos.Emails
{
    public class Email
    {
        #region Properties
        /***********************************************************/
        public string UniqueId { get; }
        public string Date { get; }
        public string Time { get; }
        public string MimeType { get; }
        public string Subject { get; }
        public ICollection<string> Names { get; } = new List<string>();
        public ICollection<string> Addresses { get; } = new List<string>();
        public string TextBody { get; set; }
        public string HtmlBody { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        public Email(
            string uniqueId,
            DateTime dt,
            string mimeType,
            string subject)
        {
            UniqueId = uniqueId;
            Date = dt.ToString("yyyy-MM-dd");
            Time = dt.ToString("HH:mm");
            MimeType = mimeType;
            Subject = subject;
        }
        #endregion

        #region Methods
        /***********************************************************/
        public void AddName(string name)
        {
            Names.Add(name);
        }

        public string GetFirstName()
        {
            return Names.First();
        }

        public void AddAddress(string address)
        {
            Addresses.Add(address);
        }

        public string GetFirstAddress()
        {
            return Addresses.First();
        }
        #endregion

        #region Methods
        /***********************************************************/
        public bool NameOrAddressContains(string value)
        {
            return NameContains(value)
                || AddressContains(value);
        }

        public bool NameContains(string value)
        {
            return Contains(Names, value);
        }

        public bool AddressContains(string value)
        {
            return Contains(Addresses, value);
        }

        public bool SubjectContains(string value)
        {
            return Subject.Contains(value);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        private bool Contains(ICollection<string> values, string value)
        {
            foreach (var item in values)
                if (item.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0)
                    return true;

            return false;
        }

        public override string ToString()
        {
            JoinerList joiner = new JoinerList("\n");

            joiner.Add(UniqueId + " " + MimeType);
            joiner.Add(Date + " " + Time);
            joiner.AddRange(Names);
            joiner.AddRange(Addresses);
            joiner.Add(Subject);
            joiner.Add(TextBody);
            joiner.Add(HtmlBody);

            return joiner.ToString();
        }
        #endregion
    }
}
