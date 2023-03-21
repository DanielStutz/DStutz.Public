using DStutz.Apps.Services.Base.Configs;

using Microsoft.Data.Sqlite;

namespace DStutz.Apps.Services.Base.SQL
{
    public abstract class ServiceSQLSqlite
        : ServiceSQLBase<ServiceConfigSQLSqlite>
    {
        #region Properties
        /***********************************************************/
        private SqliteConnection? _connection;
        public override Status Status { get; protected set; } = Status.ER;
        #endregion

        #region Constructors
        /***********************************************************/
        protected ServiceSQLSqlite(
            ServiceContext context,
            bool init = false)
            : base(context, init) { }
        #endregion

        #region Methods connecting
        /***********************************************************/
        protected override void Connect()
        {
            _connection = new SqliteConnection(Config.Connection);
            _connection.Open();

            if (GetTableCount() > 0)
                Status = Status.OK;
        }
        #endregion

        #region Methods executing
        /***********************************************************/
        protected override List<string[]> ExecuteReader(string query)
        {
            try
            {
                if (_connection == null)
                    throw new NullReferenceException();

                using SqliteCommand c = _connection.CreateCommand();
                c.CommandText = query;
                using SqliteDataReader r = c.ExecuteReader();

                List<string[]> rows = new List<string[]>();

                while (r.Read())
                {
                    string[] cells = new string[r.FieldCount];

                    for (int i = 0; i < r.FieldCount; i++)
                    {
                        cells[i] = r.GetString(i);
                    }

                    rows.Add(cells);
                }

                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Unable to execute reader for query {query}", ex);
            }
        }

        protected override T ExecuteScalar<T>(string query)
        {
            try
            {
                if (_connection == null)
                    throw new NullReferenceException();

                using SqliteCommand c = _connection.CreateCommand();
                c.CommandText = query;

                return (T)c.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Unable to execute scalar for query {query}", ex);
            }
        }
        #endregion

        #region Methods handling tables
        /***********************************************************/
        public override long GetTableCount()
        {
            return ExecuteScalar<long>(
                "SELECT count(*) " +
                "FROM sqlite_master " +
                "WHERE type = 'table' AND name NOT LIKE 'sqlite_%'");
        }

        public override IEnumerable<string> GetTableNames()
        {
            return ExecuteReader(
                "SELECT name " +
                "FROM sqlite_schema " +
                "WHERE type = 'table' AND name NOT LIKE 'sqlite_%'")
                .Select(e => e[0]);
        }
        #endregion

        #region Methods disposing
        /***********************************************************/
        public override void Dispose()
        {
            base.Dispose();

            try
            {
                if (_connection != null)
                    _connection.Close();
            }
            catch (Exception) { }
        }
        #endregion
    }
}
