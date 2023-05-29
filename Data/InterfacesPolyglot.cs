namespace DStutz.Data;

[Obsolete("See DStutz.System.Polyglot", false)]
public interface IPolyglotOLD
{
    public string FindText(string ISOCode639 = "de");
}

[Obsolete("See DStutz.System.Polyglot", false)]
public interface IDeEnOLD
{
    public string? DE { get; set; }
    public string? EN { get; set; }
}

[Obsolete("See DStutz.System.Polyglot", false)]
public interface IDeEnFrOLD
{
    public string? DE { get; set; }
    public string? EN { get; set; }
    public string? FR { get; set; }
}

[Obsolete("See DStutz.System.Polyglot", false)]
public interface IDeEnKeyOLD : IDeEnOLD
{
    public long Pk1 { get; set; }
}

[Obsolete("See DStutz.System.Polyglot", false)]
public interface IDeEnFrKeyOLD : IDeEnFrOLD
{
    public long Pk1 { get; set; }
}
