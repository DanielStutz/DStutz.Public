using DStutz.System.Exceptions;

namespace DStutz.Data.Pocos.People
{
    public abstract class SexMapper
    {
        public static Sex Map(string sex)
        {
            switch (sex)
            {
                case "M":
                    return Sex.M;
                case "F":
                    return Sex.F;
                case "I":
                    return Sex.I;
                default:
                    throw new NotFoundException("Sex", sex);
            }
        }
    }
}
