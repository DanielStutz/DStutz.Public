using DStutz.Apps.Services.Base.Configs;
using DStutz.System.IO;

using System.Text;

namespace DStutz.Apps.Services.Base.SQL
{
    public interface IServiceSQLBase
        : IService
    {
        public long Count(string table, string? where = null);
        public string[] Find(string table, string columns, string? where = null);
        public ICollection<string[]> FindAll(string table, string columns, string? where = null);
    }

    public interface IServiceSQLBase<C>
        : IServiceSQL<C>, IServiceSQLBase
        where C : ServiceConfigSQL
    {
    }

    public abstract class ServiceSQLBase<C>
        : Service<C>, IServiceSQLBase<C>
        where C : ServiceConfigSQL
    {
        #region Properties
        /***********************************************************/
        public virtual Status Status { get; protected set; } = Status.ER;
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceSQLBase(
            ServiceContext context,
            bool init = false)
            : base(context)
        {
            try
            {
                Connect();
            }
            catch (Exception ex)
            {
                Dispose();
                throw new Exception(
                    "Unable to connect to SQL database: " +
                    Config.Connection, ex);
            }

            AppLogger.LogStart(this);

            if (init)
                Init();
        }
        #endregion

        #region Methods connecting
        /***********************************************************/
        protected abstract void Connect();
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
            // TODO Test the methode ExecuteScalar<S>()
            ExecuteScalar<int>(
                InitSQL.GetSQLFromDataFiles(
                    Context,
                    encoding,
                    files)
            );
        }
        #endregion

        #region Methods executing
        /***********************************************************/
        protected abstract List<string[]> ExecuteReader(string query);
        protected abstract S ExecuteScalar<S>(string query);
        #endregion

        #region Methods handling tables
        /***********************************************************/
        public abstract long GetTableCount();
        public abstract IEnumerable<string> GetTableNames();
        #endregion

        #region Methods handling entities
        /***********************************************************/
        public long Count(
            string table,
            string? where = null)
        {
            if (string.IsNullOrWhiteSpace(table))
                throw new ArgumentException("Table");

            return ExecuteScalar<long>(Select(table, "COUNT(*)", where));
        }

        public string[] Find(
            string table,
            string columns,
            string? where = null)
        {
            if (string.IsNullOrWhiteSpace(table))
                throw new ArgumentException("Table");

            if (string.IsNullOrWhiteSpace(columns))
                throw new ArgumentException("Columns");

            return ExecuteReader(Select(table, columns, where))[0];
        }

        public ICollection<string[]> FindAll(
            string table,
            string columns,
            string? where = null)
        {
            if (string.IsNullOrWhiteSpace(table))
                throw new ArgumentException("Table");

            if (string.IsNullOrWhiteSpace(columns))
                throw new ArgumentException("Columns");

            return ExecuteReader(Select(table, columns, where));
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        protected string Select(
            string table,
            string columns,
            string? where = null)
        {
            return "SELECT " + columns +
                " FROM " + table +
                (where != null ? " WHERE " + where : "");
        }
        #endregion
    }
}
