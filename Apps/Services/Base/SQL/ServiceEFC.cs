using DStutz.Apps.Services.Base.Configs;

using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DStutz.Apps.Services.Base.SQL
{
    public interface IServiceEFC
        : IService
    {
        public int SaveChanges();
        public Task<int> SaveChangesAsync();
    }

    public interface IServiceEFC<C>
        : IServiceSQL<C>, IServiceEFC
        where C : ServiceConfigSQL
    {
    }

    public abstract class ServiceEFC<C>
        : DbContext, IServiceEFC<C>
        where C : ServiceConfigSQL
    {
        #region Properties
        /***********************************************************/
        public ServiceContext Context { get; }
        public List<ServiceMessage> Messages { get; } = new();
        public ILogger Logger { get; }
        public C Config { get; }
        public virtual Status Status { get; protected set; } = Status.ER;
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceEFC(
            ServiceContext context,
            bool init = false)
        {
            Context = context;
            Logger = context.CreateLogger(this);
            Config = context.GetServiceConfig<C>();

            if (Database.CanConnect())
                Status = Status.OK;

            AppLogger.LogStart(this);

            if (init)
                Init();
        }
        #endregion

        #region Methods initializing
        /***********************************************************/
        protected virtual void Init()
        {
            throw new NotImplementedException();
        }

        protected void Init(
            Encoding encoding,
            params string[] files)
        {
            ExecuteSqlRaw(
                InitSQL.GetSQLFromDataFiles(
                    Context,
                    encoding,
                    files)
            );
        }
        #endregion

        #region Methods configuring
        /***********************************************************/
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                if (Config.UseSqlite)
                {
                    // Uses Microsoft.EntityFrameworkCore.Sqlite
                    optionsBuilder.UseSqlite(Config.Connection);
                }
                else if (Config.UseMySql)
                {
                    // Uses MySql.EntityFrameworkCore
                    optionsBuilder.UseMySQL(Config.Connection);
                }
                //else if (Config.UseNpgSql)
                //{
                //    // Uses Npgsql.EntityFrameworkCore.PostgreSQL
                //    optionsBuilder.UseNpgsql(Config.Connection);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Unable to configure EFC database", ex);
            }
        }

        // Used (temporarily) in method OnConfiguring() of ServiceProducts
        protected static ILoggerFactory ContextLoggerFactory
            => LoggerFactory.Create(b => b.AddConsole().AddFilter("", LogLevel.Information));
        #endregion

        #region Methods executing
        /***********************************************************/
        public int ExecuteSqlRaw(string query)
        {
            return Database.ExecuteSqlRaw(query);
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public virtual void Test()
        {
            throw new NotImplementedException();
        }

        public string? ToString(DateTime? date)
        {
            return ToString(date, "yyyy-MM-dd");
        }

        public string? ToString(DateTime? date, string format)
        {
            if (date == null)
                return null;

            return ((DateTime)date).ToString(format);
        }

        public DateTime ToDate(string? date)
        {
            if (date == null)
                return DateTime.MinValue;

            return DateTime.Parse(date);
        }

        public DateTime? ToDateNullable(string? date)
        {
            if (date == null)
                return null;

            return DateTime.Parse(date);
        }
        #endregion

        #region Methods disposing
        /***********************************************************/
        public override void Dispose()
        {
            base.Dispose();

            AppLogger.LogStop(this);
        }
        #endregion
    }
}
