using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class UserRepository : Repository<PlataformaRio2CContext, User>, IUserRepository
    {
        public UserRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override void Delete(User entity)
        {
            entity.Roles.Clear();

            if (entity.UserUseTerms != null && entity.UserUseTerms.Any())
            {
                var terms = entity.UserUseTerms.ToList();

                foreach (var term in terms)
                {
                    _context.Entry(term).State = EntityState.Deleted;
                }
            }

            base.Delete(entity);
        }
    }
}
