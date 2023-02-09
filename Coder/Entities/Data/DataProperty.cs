namespace DStutz.Coder.Entities.Data
{
    public abstract class DataProperty
    {
        #region Properties
        /***********************************************************/
        public string Name { get; set; }
        public bool IsOptional { get; set; }
        public DataType Type { get; set; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataProperty(
            string name,
            string type)
        {
            Name = name;
            IsOptional = type.EndsWith("?");
            Type = new DataType(type);
        }

        protected DataProperty(
            JsonProperty property)
        {
            Name = property.Name;
            IsOptional = property.Type.EndsWith("?");
            Type = new DataType(property);
        }

        protected DataProperty(
            JsonRelationMtoN property)
        {
            Name = property.Name;
            IsOptional = property.Type.EndsWith("?");
            Type = new DataType(property);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public virtual string[] GetAssign()
        {
            return new string[] {
                $"{Name} = e1.{Name},",
            };
        }

        public virtual string[] GetMappingE2P()
        {
            return new string[] {
                $"poco.{Name} =",
                $"    Mapper.{GetMapperMethod()}(",
                $"        efco.{Name},",
                $"        e => e.Map());",
                "",
            };
        }

        public virtual string[] GetMappingP2E()
        {
            return new string[] {
                $"efco.{Name} =",
                $"    Mapper.{GetMapperMethod()}(",
                $"        poco.{Name},",
                $"        e => e.Map<{Type.E}>());",
                "",
            };
        }

        public virtual string GetMapperMethod()
        {
            return IsOptional ? "MapOptional" : "MapMandatory";
        }

        public virtual string[] GetProperty()
        {
            return new string[] {
                GetProperty(Type.N, Name, IsOptional),
            };
        }

        public abstract string[] GetPropertyEfco();

        public virtual string[] GetPropertyPoco()
        {
            return new string[] {
                GetProperty(Type.P, Name, IsOptional),
            };
        }

        protected string GetProperty(
            string type,
            string name,
            bool isOptional)
        {
            if (isOptional)
                type += "?";

            return $"public {type} {name} {{ get; set; }}";
        }
        #endregion
    }
}
