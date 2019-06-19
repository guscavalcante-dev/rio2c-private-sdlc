using PlataformaRio2C.Infra.Data.Context.Models;

namespace PlataformaRio2C.Infra.Data.Context.Interfaces
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        SaveChangesResult SaveChanges();
    }
}
