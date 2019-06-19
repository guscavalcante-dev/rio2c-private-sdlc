using PlataformaRio2C.Infra.Data.Context.Interfaces;

namespace PlataformaRio2C.Application.Interfaces
{
    public interface ITransactionAppService<TContext>
         where TContext : IDbContext, new()
    {
        void BeginTransaction();
        void Commit();
    }
}
