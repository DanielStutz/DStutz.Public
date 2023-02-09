using System.Collections.Generic;

namespace DStutz.Coder.Entities.Data
{
    public abstract class DataRelationList : DataProperty
    {
        #region Properties
        /***********************************************************/
        private string ListType { get; }
        protected string ListTypeEfco { get; private set; }
        protected string ListTypePoco { get; private set; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataRelationList(
            JsonRelation1toN property)
            : base(property)
        {
            if (property.ListType.Contains("?"))
                throw new JsonOptionalException(
                    "Relations1toN",
                    "ListType");

            ListType = property.ListType;
        }

        protected DataRelationList(
            JsonRelationMtoN property)
            : base(property)
        {
            if (property.ListType.Contains("?") ||
                property.JunctionType.Contains("?"))
                throw new JsonOptionalException(
                    "RelationsMtoN",
                    "ListType",
                    "JunctionType");

            ListType = property.ListType;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        protected void AddEfco(
            string efco)
        {
            ListTypeEfco = $"{ListType}<{efco}>";
        }

        protected void AddPoco(
            string poco)
        {
            ListTypePoco = $"{ListType}<{poco}>";
        }

        public override string GetMapperMethod()
        {
            return IsOptional ? "MapOptionals" : "MapMandatories";
        }
        #endregion
    }
}
