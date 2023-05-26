// Licensed ...

using System.Text.RegularExpressions;

namespace DStutz.Math.Sets.Numbers;

public interface INumberSet<T>
    where T : IParsable<T>
{
    public int Count { get; }
    public bool IsEmpty { get; }
    public T Min { get; }
    public T Max { get; }
    public bool Contains(T number);
    public T ContainsOrThrow(T number);
}

public abstract class NumberSet<T>
    : INumberSet<T>
    where T : IParsable<T>
{
    #region Properties
    /***********************************************************/
    public abstract int Count { get; }
    public abstract bool IsEmpty { get; }
    public abstract T Min { get; }
    public abstract T Max { get; }
    protected char LeftBracket { get; set; }
    protected char RightBracket { get; set; }
    #endregion

    #region Constructors
    /***********************************************************/
    protected NumberSet(
        string set)
    {
        set = Regex.Replace(set, @"\s+", "");

        try
        {
            LeftBracket = set.First();
            RightBracket = set.Last();
            Check(LeftBracket, RightBracket);
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Unable to parse brackets of {set}", ex);
        }

        try
        {
            var numbers = set.Substring(1, set.Length - 2);

            if (!numbers.Contains(','))
                throw new Exception(
                    "Use commas to delimit numbers");

            Parse(numbers.Split(","));
        }
        catch (Exception ex)
        {
            throw new Exception(
                $"Unable to parse numbers of {set}", ex);
        }
    }
    #endregion

    #region Methods implementing
    /***********************************************************/
    public abstract bool Contains(T number);

    public T ContainsOrThrow(
        T number)
    {
        if (Contains(number))
            return number;

        throw new Exception($"{number} ∉ {this}");
    }
    #endregion

    #region Methods
    /***********************************************************/
    protected abstract void Check(char leftBracket, char rightBracket);
    protected abstract void Parse(string[] numbers);
    #endregion

    #region Miscellaneous
    /***********************************************************/
    protected string AddBrackets(
        string content)
    {
        return $"{LeftBracket}{content}{RightBracket}";
    }
    #endregion
}
