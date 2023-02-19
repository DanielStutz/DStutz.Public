using DStutz.Coder.Entities.Data;

namespace DStutz.Coder.Entities.EntityBasic
{
    public class FileEntityBasic : FileEntity
    {
        #region Template
        /***********************************************************/
        private static readonly string Template = @"
WARNING
using DStutz.Data;
using DStutz.System.Joiners;

using NAMESPACE_EFCO;

// Version VERSION
namespace NAMESPACE_POCO
{
CURSOR_INTERFACE

CURSOR_POCO
}

----

WARNING
using DStutz.Data;
using DStutz.System.Joiners;

using NAMESPACE_POCO;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Version VERSION
namespace NAMESPACE_EFCO
{
CURSOR_EFCO

CURSOR_MAPPER
}";
        #endregion

        #region Constructors
        /***********************************************************/
        public FileEntityBasic(
            DataEntityBasic data)
            : base(Template, data)
        {
            Insert(new CodeInterface(data), "INTERFACE", 4);
            Insert(new CodePoco(data), "POCO", 4);
            Insert(new CodeEfco(data), "EFCO", 4);
            Insert(new CodeMapper(data), "MAPPER", 4);
            PostProcessing();

            //Write(false, false);
        }
        #endregion
    }
}
