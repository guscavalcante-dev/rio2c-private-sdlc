using PlataformaRio2C.Infra.Data.Context.Models;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public interface IUnitOfWorkSystemParameter
    {
        void BeginTransaction();
        SaveChangesResult SaveChanges();
    }
}
