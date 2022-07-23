namespace FinLib.DataLayer.Context
{
    public interface ITransactionProvider
    {
        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void DisposeTransaction();
    }
}