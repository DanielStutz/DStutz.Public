using DStutz.Data;

namespace DStutz.Coder.Entities.Data
{
    public abstract class DataProperty<T>
        where T : DataType
    {
        #region Properties
        /***********************************************************/
        public string Name { get; }
        public bool IsOptional { get; }
        public T Type { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataProperty(
            string name,
            bool isOptional,
            T type)
        {
            Name = name;
            IsOptional = isOptional;
            Type = type;
        }

        protected DataProperty(
            JsonProperty property,
            T type)
        {
            Name = property.Name;
            IsOptional = property.IsOptional;
            Type = type;
        }
        #endregion

        #region Methods mapping
        /***********************************************************/
        public virtual string[] GetMappingE2P()
        {
            return new string[] {
                $"poco.{Name} =",
                $"    {Mapper.GetMethod(IsOptional, Type.IsCollection)}(",
                $"        efco.{Name},",
                $"        e => e.Map());",
                "",
            };
        }

        public virtual string[] GetMappingP2E()
        {
            return GetMappingP2E(Type.E);
        }

        protected string[] GetMappingP2E(
            params string[] types)
        {
            return new string[] {
                $"efco.{Name} =",
                $"    {Mapper.GetMethod(IsOptional, Type.IsCollection)}(",
                $"        poco.{Name},",
                $"        e => e.Map<{string.Join(", ", types)}>());",
                "",
            };
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
