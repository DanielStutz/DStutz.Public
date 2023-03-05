namespace DStutz.Data.Pocos.People
{
    public abstract class GenderMapper
    {
        private static Gender Mapper { get; } = new Gender();

        public static Gender Map(string gender)
        {
            return Mapper.Map(gender);
        }
    }
}
