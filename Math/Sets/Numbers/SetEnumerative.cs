// Licensed ...

using System.Collections;
using System.Numerics;

namespace DStutz.Math.Sets.Numbers;

public interface ISetEnumerative<T>
    : INumberSet<T>, IEnumerable<T>
    where T : struct, INumber<T>
{
    public bool Add(T element);
    public void AddRange(IEnumerable<T> numbers);
    public void Clear();
    public bool Remove(T item);
}

public class SetEnumerative<T>
    : NumberSet<T>, ISetEnumerative<T>
    where T : struct, INumber<T>
{
    #region Properties
    /***********************************************************/
    private SortedSet<T> Elements { get; } = new SortedSet<T>();
    #endregion

    #region Constructors
    /***********************************************************/
    public SetEnumerative(
        string set)
        : base(set) { }
    #endregion

    #region Properties implementing
    /***********************************************************/
    public override int Count
    {
        get { return Elements.Count; }
    }

    public override bool IsEmpty
    {
        get { return Elements.Count == 0; }
    }

    public override T Min
    {
        get
        {
            if (Elements.Count > 0)
                return Elements.First();

            throw new Exception($"{this} is empty");
        }
    }

    public override T Max
    {
        get
        {
            if (Elements.Count > 0)
                return Elements.Last();

            throw new Exception($"{this} is empty");
        }
    }
    #endregion

    #region Methods implementing
    /***********************************************************/
    protected override void Check(
        char leftBracket,
        char rightBracket)
    {
        if (leftBracket != '{')
            throw new Exception(
                "Use '{' as left bracket");

        if (rightBracket != '}')
            throw new Exception(
                "Use '{' as right bracket");
    }

    protected override void Parse(
        string[] numbers)
    {
        foreach (var number in numbers)
            Elements.Add(T.Parse(number, null));
    }

    public bool Add(
        T number)
    {
        return Elements.Add(number);
    }

    public void AddRange(
        IEnumerable<T> numbers)
    {
        foreach (var number in numbers)
            Elements.Add(number);
    }

    public void Clear()
    {
        Elements.Clear();
    }

    public override bool Contains(
        T number)
    {
        return Elements.Contains(number);
    }

    public bool Remove(T number)
    {
        return Elements.Remove(number);
    }
    #endregion

    #region Methods implementing IEnumerable<T>
    /***********************************************************/
    public IEnumerator<T> GetEnumerator()
    {
        return Elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
    #endregion

    #region Miscellaneous
    /***********************************************************/
    public override string ToString()
    {
        return AddBrackets(string.Join(",", Elements));
    }
    #endregion
}
