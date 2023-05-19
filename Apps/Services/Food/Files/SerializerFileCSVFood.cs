using DStutz.System.Serializers;

using System.Text;

namespace DStutz.Apps.Services.Food.Files
{
    public class SerializerFileCSVFood
        : SerializerFileCSV<FoodItemCSV>
    {
        #region Constructors
        /***********************************************************/
        public SerializerFileCSVFood(
            string fileID,
            string filePath)
            : base(
                  fileID,
                  new FileInfo(filePath),
                  129,
                  "\t",
                  true)
        {
            FileEncoding = Encoding.Latin1;
        }
        #endregion

        #region Methods deserializing
        /***********************************************************/
        public override FoodItemCSV Deserialize(string line)
        {
            var cells = CheckAndSplit(line);

            try
            {
                return new FoodItemCSV(cells);
            }
            catch (Exception ex)
            {
                throw new DeserializeException(line, cells, ex);
            }
        }
        #endregion
    }
}
