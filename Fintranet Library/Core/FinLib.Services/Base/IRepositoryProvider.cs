using FinLib.DataLayer.Context;

namespace FinLib.Services.Base
{
    internal interface IRepositoryProvider
    {
        IUnitOfWork DbContext { get; }
    }
}
