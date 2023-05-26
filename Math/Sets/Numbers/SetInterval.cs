// Licensed ...

// See https://en.wikipedia.org/wiki/Interval_(mathematics)

using System.Numerics;

namespace DStutz.Math.Sets.Numbers;

public interface ISetInterval<T>
    : INumberSet<T>
    where T : struct, INumber<T>
{
    public T LowerBorder { get; }
    public T UpperBorder { get; }
    public bool IsClosed { get; }
    public bool IsLeftClosed { get; }
    public bool IsRightClosed { get; }
    public bool IsDegenerated { get; }
    public bool IsOpen { get; }
    public bool IsHalfOpen { get; }
    public bool IsLeftOpen { get; }
    public bool IsRightOpen { get; }
}

public class SetInterval<T>
    : NumberSet<T>, ISetInterval<T>
    where T : struct, INumber<T>
{
    #region Properties
    /***********************************************************/
    public T LowerBorder { get; private set; }
    public T UpperBorder { get; private set; }
    private bool IncludeLowerBorder { get; set; }
    private bool IncludeUpperBorder { get; set; }
    #endregion

    #region Constructors
    /***********************************************************/
    public SetInterval(
        string set)
        : base(set) { }
    #endregion

    #region Properties implementing
    /***********************************************************/
    public override int Count
    {
        get
        {
            if (IsEmpty)
                return 0;

            if (IsDegenerated)
                return 1;

            throw new Exception(
                $"{this} has infinit numbers");
        }
    }

    public bool IsClosed
    {
        get { return IsLeftClosed && IsRightClosed; }
    }

    public bool IsLeftClosed
    {
        get { return IncludeLowerBorder; }
    }

    public bool IsRightClosed
    {
        get { return IncludeUpperBorder; }
    }

    public bool IsDegenerated
    {
        get { return BordersMatch && IsClosed; }
    }

    public override bool IsEmpty
    {
        get { return BordersMatch && !IsClosed; }
    }

    public bool IsOpen
    {
        get { return IsLeftOpen && IsRightOpen; }
    }

    public bool IsHalfOpen
    {
        get { return IsLeftOpen ^ IsRightOpen; }
    }

    public bool IsLeftOpen
    {
        get { return !IncludeLowerBorder; }
    }

    public bool IsRightOpen
    {
        get { return !IncludeUpperBorder; }
    }

    public override T Min
    {
        get
        {
            if (IncludeLowerBorder)
                return LowerBorder;

            throw new Exception(
                $"{this} has no min as it is left-open");
        }
    }

    public override T Max
    {
        get
        {
            if (IncludeUpperBorder)
                return UpperBorder;

            throw new Exception(
                $"{this} has no max as it is right-open");
        }
    }
    #endregion

    #region Methods implementing
    /***********************************************************/
    protected override void Check(
        char leftBracket,
        char rightBracket)
    {
        if (LeftBracket == '[')
            IncludeLowerBorder = true;
        else if (
            LeftBracket == '(' ||
            LeftBracket == ']')
            IncludeLowerBorder = false;
        else
            throw new Exception(
                "Use '[', '(' or ']' as left bracket");

        if (RightBracket == ']')
            IncludeUpperBorder = true;
        else if (
            RightBracket == ')' ||
            RightBracket == '[')
            IncludeUpperBorder = false;
        else
            throw new Exception(
                "Use ']', ')' or '[' as right bracket");
    }

    protected override void Parse(
        string[] numbers)
    {
        LowerBorder = T.Parse(numbers[0], null);
        UpperBorder = T.Parse(numbers[1], null);

        if (LowerBorder > UpperBorder)
            throw new Exception(
                $"The lower border {LowerBorder}" +
                "must be less than " +
                $"the upper border {UpperBorder}" +
                "or equal");
    }

    public override bool Contains(
        T number)
    {
        if (LowerBorder < number && number < UpperBorder)
            return true;

        if (IncludeLowerBorder && number == LowerBorder)
            return true;

        if (IncludeUpperBorder && number == UpperBorder)
            return true;

        return false;
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    private bool BordersMatch
    {
        get { return LowerBorder == UpperBorder; }
    }

    public override string ToString()
    {
        return AddBrackets($"{LowerBorder},{UpperBorder}");
    }
    #endregion
}
