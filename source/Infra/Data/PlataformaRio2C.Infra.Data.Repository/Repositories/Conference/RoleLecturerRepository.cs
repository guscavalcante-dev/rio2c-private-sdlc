//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using System.Data.Entity;
//using System.Linq;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    public class RoleLecturerRepository : Repository<PlataformaRio2CContext, RoleLecturer>, IRoleLecturerRepository
//    {
//        public RoleLecturerRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//        }

//        public override IQueryable<RoleLecturer> GetAll(bool @readonly = false)
//        {
//            var consult = this.dbSet
//                                .Include(i => i.Titles)
//                                .Include(i => i.Titles.Select(e => e.Language));


//            return @readonly
//              ? consult.AsNoTracking()
//              : consult;
//        }

//        public override IQueryable<RoleLecturer> GetAllSimple()
//        {
//            return this.dbSet
//                                .Include(i => i.Titles)
//                                .Include(i => i.Titles.Select(e => e.Language))
//                                .AsNoTracking();
//        }


//        public override void Delete(RoleLecturer entity)
//        {
//            if (entity.Titles != null && entity.Titles.Any())
//            {
//                foreach (var item in entity.Titles.ToList())
//                {
//                    _context.Entry(item).State = EntityState.Deleted;
//                }

//                entity.Titles.Clear();
//            }

//            base.Delete(entity);
//        }

//    }
//}
