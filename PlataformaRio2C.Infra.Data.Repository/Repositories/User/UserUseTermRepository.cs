using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class UserUseTermRepository : Repository<PlataformaRio2CContext, UserUseTerm>, IUserUseTermRepository
    {
        public UserUseTermRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public IQueryable<UserUseTerm> GetAll()
        {

            return this.dbSet
                        .Include(q => q.Role)
                        .Include(i => i.User)
                        .Include(i => i.Event)
                        .AsNoTracking();
        }

        public override IQueryable<UserUseTerm> GetAll(Expression<Func<UserUseTerm, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }
    }
}
