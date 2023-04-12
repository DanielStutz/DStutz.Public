using DStutz.System.Commands;
using DStutz.System.IO;
using System.Text;

namespace DStutz.Apps.Services.Base.SQL
{
    public abstract class InitSQL
    {
        public static string GetSQLFromDataFiles(
            ServiceContext context,
            Encoding encoding,
            params string[] files)
        {
            var logger = context.CreateLogger("InitSQL");
            var io = context.AppContext.AppConfig;

            logger.LogInformation(
                "--> Database (initialize sql from files):");

            logger.LogInformation(
                "    --> Dir:  {0}",
                io.GetDataDir());

            var sql = "";

            foreach (var file in files)
            {
                logger.LogInformation(
                    "    --> File: {0}",
                    file);

                sql += FileReader.ReadAllTextRequired(
                    io.GetDataFile(file),
                    encoding
                );
            }

            //logger.LogInformation(
            //    "--> Initializing sql:\n{0}",
            //    sql);

            return sql;
        }

        public static void Init(
            IAppIO io,
            string workDir,
            string tempFile,
            string dataFile,
            IEnumerable<string> initFiles)
        {
            var sql = "";

            // TODO Add encoding in method signature
            foreach (var initFile in initFiles)
                sql += FileReader.ReadAllTextRequired(
                    io.GetDataFile(initFile),
                    Encoding.UTF8);

            sql += "\n.bail on\ndummy";

            FileWriter.WriteAllText(
                sql,
                io.GetDataFile(workDir + "/" + tempFile));

            Console.WriteLine(sql);

            CommandSqlite cs = new CommandSqlite();

            cs.Init(
                io.GetDataDir(workDir),
                tempFile,
                dataFile);
        }
    }
}
