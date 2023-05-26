global using DStutz.System.Joiners;
global using DStutz.System.Unique;

global using static DStutz.Constants;

namespace DStutz;

public static class Constants
{
    public static class CInclude
    {
        public const int None = int.MinValue;
        public const int All = int.MaxValue;

        // See DStutz.System.Unique for assignments
        public const int Images = 19;
        public const int Names = 24;
    }

    public static class CMath
    {
        public const long E3 = 1_000;
        public const long E4 = 10_000;
        public const long E5 = 100_000;
        public const long E6 = 1_000_000;
        public const long E7 = 10_000_000;
        public const long E8 = 100_000_000;
        public const long E9 = 1_000_000_000;
        public const long E10 = 10_000_000_000;
        public const long E11 = 100_000_000_000;
        public const long E12 = 1_000_000_000_000;
    }
}
