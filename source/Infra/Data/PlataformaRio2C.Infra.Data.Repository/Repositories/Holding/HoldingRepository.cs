using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class HoldingRepository : Repository<PlataformaRio2CContext, Holding>, IHoldingRepository
    {
        public HoldingRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Holding> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Image)
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(t => t.Language));
                              

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Holding> GetAll(Expression<Func<Holding, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Holding Get(Expression<Func<Holding, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Holding Get(object id)
        {
            return this.dbSet
                 .Include(i => i.Image)
                .SingleOrDefault(x => x.Id == (int)id);

        }
    }
}
