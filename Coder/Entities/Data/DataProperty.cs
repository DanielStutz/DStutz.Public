using DStutz.Data;

namespace DStutz.Coder.Entities.Data
{
    public abstract class DataProperty<T>
        where T : DataType
    {
        #region Properties
        /***********************************************************/
        public string Name { get; }
        public string? Comment { get; }
        public bool IsOptional { get; } = false;
        public T Type { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        protected DataProperty(
            string name,
            JsonKey key,
            T type)
        {
            Name = name;
            Comment = key.Comment;
            Type = type;
        }

        protected DataProperty(
            JsonProperty property,
            T type)
        {
            Name = property.Name;
            Comment = property.Comment;
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
                GetSetProperty(Type.N, Name, IsOptional),
            };
        }

        //public abstract string[] GetPropertyEfco();
        public virtual string[] GetPropertyEfco()
        {
            return new string[] {
                GetSetProperty(Type.E, Name, IsOptional),
            };
        }

        public virtual string[] GetPropertyPoco()
        {
            return new string[] {
                GetSetProperty(Type.P, Name, IsOptional),
            };
        }

        protected string GetSetProperty(
            string type,
            string name,
            bool isOptional)
        {
            if (isOptional)
                type += "?";

            var line = $"public {type} {name} {{ get; set; }}";

            if (Comment != null)
                line += $" // {Comment}";

            return line;
        }
        #endregion
    }
}
