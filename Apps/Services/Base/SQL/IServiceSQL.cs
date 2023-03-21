using DStutz.Apps.Services.Base.Configs;

namespace DStutz.Apps.Services.Base.SQL
{
    public enum Status : int
    {
        ER = -1,
        OK = 0,
    }

    public interface IServiceSQL<C>
        : IService<C>
        where C : ServiceConfigSQL
    {
        public Status Status { get; }
    }
}
