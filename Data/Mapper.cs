namespace DStutz.Data
{
    public abstract class Mapper
    {
        #region Methods checking
        /***********************************************************/
        public static void CheckForWhiteSpaces(
            string entity,
            string? content)
        {
            if (content != null &&
                content.Any(char.IsWhiteSpace))
                throw new Exception(
                    $"{entity} contains at least one whitespace");
        }
        #endregion

        #region Methods naming mapper methods, see below
        /***********************************************************/
        public static string GetMethod(
            bool isOptional,
            bool isCollection)
        {
            var name = typeof(Mapper).Name + ".Map";

            if (isCollection)
                name += isOptional ? "Optionals" : "Mandatories";
            else
                name += isOptional ? "Optional" : "Mandatory";

            return name;
        }
        #endregion

        #region Methods mapping a single entity
        /***********************************************************/
        public static T MapMandatory<S, T>(
            S item,
            Func<S, T> selector)
            where T : class
        {
            return selector(item);
        }

        public static T? MapOptional<S, T>(
            S? item,
            Func<S, T> selector)
            where T : class
        {
            if (item == null)
                return null;

            return selector(item);
        }
        #endregion

        #region Methods mapping a list of entities
        /***********************************************************/
        public static List<T> MapMandatories<S, T>(
            IReadOnlyList<S> list,
            Func<S, T> selector)
        {
            return new List<T>(list.Select(selector));
        }

        public static List<T>? MapOptionals<S, T>(
            IReadOnlyList<S>? list,
            Func<S, T> selector)
        {
            if (list == null ||
                list.Count == 0)
                return null;

            return new List<T>(list.Select(selector));
        }

        public static List<T>? MapOptionalsOrdered<S, T>(
            IReadOnlyList<S>? list1,
            Func<S, T> selector)
            where T : IOrdered
        {
            var list2 = MapOptionals(list1, selector);

            if (list2 != null)
                for (int i = 0; i < list2.Count; i++)
                    list2[i].OrderBy = i + 1;

            return list2;
        }
        #endregion
    }
}
