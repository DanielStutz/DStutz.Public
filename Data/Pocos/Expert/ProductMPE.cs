using DStutz.Data.Efcos.Expert;

// Version 1.1.0
namespace DStutz.Data.Pocos.Expert
{
    public interface IProduct
    {
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public long? ProducerPk1 { get; set; }
    }

    public class ProductMPE
    {
        #region Properties
        /***********************************************************/
        public long Pk1 { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        #endregion

        #region Relations m:1 (with specific foreign key)
        /***********************************************************/
        public long? ProducerPk1 { get; set; }
        public ProducerMPE? Producer { get; set; }
        #endregion
    }
}
