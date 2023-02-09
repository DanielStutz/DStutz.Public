namespace DStutz.Coder.Entities.Data
{
    public class DataRelation1toN : DataRelationList
    {
        #region Title
        /***********************************************************/
        public static string Title = "Relations 1:n (with default foreign key)";
        #endregion

        #region Properties
        /***********************************************************/
        private string ForeignKey { get; } = "Pk1";
        #endregion

        #region Constructors
        /***********************************************************/
        public DataRelation1toN(
            JsonRelation1toN property)
            : base(property)
        {
            // List<CommentMEE>
            AddEfco(Type.E);

            // List<CommentMPE>
            AddPoco(Type.P);

            Console.WriteLine("");
            Console.WriteLine(GetProperty(ListTypeEfco, Name, IsOptional));
            Console.WriteLine(GetProperty(ListTypePoco, Name, IsOptional));
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override string[] GetPropertyEfco()
        {
            // [ForeignKey("Pk1")]
            // public List<CommentMEE>? Comments { get; set; }
            return new string[] {
                $"[ForeignKey(\"{ForeignKey}\")]",
                GetProperty(ListTypeEfco, Name, IsOptional),
                "",
            };
        }

        public override string[] GetPropertyPoco()
        {
            // public List<CommentMPE>? Comments { get; set; }
            return new string[] {
                GetProperty(ListTypePoco, Name, IsOptional),
            };
        }
        #endregion
    }
}
