using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DStutz.Data.Efcos
{
    [Table("config")]
    public class ConfigEE
        : IJoinableOld
    {
        #region Properties
        /***********************************************************/
        [Column("pk1"), Key]
        public int Pk1 { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("value")]
        public string Value { get; set; }
        #endregion

        #region Properties implementing
        /***********************************************************/
        public IJoiner Joiner
        {
            get {
                return new Joiner(
                    (20, Pk1),
                    (40, Key),
                    (40, Value)
                );
            }
        }
        #endregion
    }
}
