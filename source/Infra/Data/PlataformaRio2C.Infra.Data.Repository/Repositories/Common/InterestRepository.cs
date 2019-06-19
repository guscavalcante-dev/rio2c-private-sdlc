using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class InterestRepository : Repository<PlataformaRio2CContext, Interest>, IInterestRepository
    {
        public InterestRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Interest> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.InterestGroup);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Interest> GetAll(Expression<Func<Interest, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }


        public override Interest Get(Expression<Func<Interest, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Interest Get(Guid uid)
        {
            return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        }

        public override Interest Get(object id)
        {
            return this.dbSet
                .Include(i => i.InterestGroup)
                .SingleOrDefault(x => x.Id == (int)id);
            
        }

       
    }
}
