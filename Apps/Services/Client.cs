using DStutz.Apps.Utils;
using DStutz.Data.Pocos.Companies;

using System.Text.Json.Serialization;

namespace DStutz.Apps.Services
{
    public class Client
    {
        #region Properties
        /***********************************************************/
        public Info Info { get; set; }
        public Company Company { get; set; }

        [JsonPropertyName("Services")]
        public ServiceConfigs ServiceConfigs { get; set; }
        #endregion

        #region Methods
        /***********************************************************/
        public int GetFoundingYear()
        {
            return Info.Date.Year;
        }
        #endregion
    }
}
