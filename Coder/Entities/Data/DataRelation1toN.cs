namespace DStutz.Coder.Entities.Data
{
    public class DataRelation1toN : DataProperty<DataTypeList>
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
            : base(
                  property,
                  new DataTypeList(property, property.ListType))
        {
            if (property.ListType.Contains("?"))
                throw new JsonOptionalException(
                    "Relations1toN",
                    "ListType");

            // List<CommentMEE>
            Type.AddEfco(Type.E);

            // List<CommentMPE>
            Type.AddPoco(Type.P);

            Console.WriteLine("");
            Console.WriteLine(GetProperty(Type.LE, Name, IsOptional));
            Console.WriteLine(GetProperty(Type.LP, Name, IsOptional));
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
                GetProperty(Type.LE, Name, IsOptional),
                "",
            };
        }

        public override string[] GetPropertyPoco()
        {
            // public List<CommentMPE>? Comments { get; set; }
            return new string[] {
                GetProperty(Type.LP, Name, IsOptional),
            };
        }
        #endregion
    }
}
