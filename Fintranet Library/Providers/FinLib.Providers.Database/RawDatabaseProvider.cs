using FinLib.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace FinLib.Providers.Database
{
    public class RawDatabaseProvider : IRawDatabaseProvider
    {
        public QueryFactory QueryProvider { get; private set; }
        public string ConnectionString { get; private set; }

        public RawDatabaseProvider(string connectionString)
        {
            ConnectionString = connectionString;
            initQueryProvider();
        }

        public RawDatabaseProvider(string databaseName, IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString(databaseName);
            initQueryProvider();
        }

        private void initQueryProvider()
        {
            var compiler = new SqlServerCompiler();
            QueryProvider = new QueryFactory(new SqlConnection(ConnectionString), compiler);
        }
    }
}
