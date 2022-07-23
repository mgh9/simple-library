using SqlKata.Execution;

namespace FinLib.Providers.Database
{
    public interface IRawDatabaseProvider
    {
        QueryFactory QueryProvider { get; }
        string ConnectionString { get; }
    }
}
