using DStutz.Math.Sets.Numbers;

namespace DStutz.Apps.Controllers.API
{
    public abstract class APIReader
    {
        public static string? TextOrNull(
            string? text)
        {
            if (text == null)
                return null;

            return Text(text);
        }

        public static string Text(
            string? text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            text = text.Trim();

            if (text.Length == 0)
                throw new Exception(
                    "Text is empty or contains white-spaces only");

            if (text.Contains("\n") ||
                text.Contains("\r"))
                throw new Exception(
                    "Text contains new line(s)");

            return text;
        }

        public static T? GetNumberOrNullOrThrow<T>(
            INumberSet<T> set,
            T? number)
            where T : struct, IParsable<T>
        {
            if (number == null)
                return null;

            return GetNumberOrThrow(set, number);
        }

        public static T GetNumberOrThrow<T>(
            INumberSet<T> set,
            T? number)
            where T : struct, IParsable<T>
        {
            if (number == null)
                throw new ArgumentNullException(nameof(number));

            return set.ContainsOrThrow((T)number);
        }
    }
}
