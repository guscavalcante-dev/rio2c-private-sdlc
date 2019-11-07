//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using System.Linq;
//using System.Data.Entity;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    public class ProjectInterestRepository : Repository<PlataformaRio2CContext, ProjectInterest>, IProjectInterestRepository
//    {
//        public ProjectInterestRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//        }

//        public override IQueryable<ProjectInterest> GetAll(bool @readonly = false)
//        {
//            var consult = this.dbSet
//                                .Include(i => i.Interest)
//                                .Include(i => i.Project);

//            return @readonly
//              ? consult.AsNoTracking()
//              : consult;
//        }
//    }
//}
