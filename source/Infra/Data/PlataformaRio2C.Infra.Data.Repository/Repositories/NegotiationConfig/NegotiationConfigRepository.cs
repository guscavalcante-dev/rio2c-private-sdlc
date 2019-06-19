using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class NegotiationConfigRepository : Repository<PlataformaRio2CContext, NegotiationConfig>, INegotiationConfigRepository
    {
        public NegotiationConfigRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<NegotiationConfig> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Rooms)
                                .Include(i => i.Rooms.Select(e => e.Room))
                                .Include(i => i.Rooms.Select(e => e.Room.Names))
                                .Include(i => i.Rooms.Select(e => e.Room.Names.Select(r => r.Language)));

            return @readonly
              ? consult.AsNoTracking()
              : consult;

        }


        public override void Delete(NegotiationConfig entity)
        {
            if (entity.Rooms != null && entity.Rooms.Any())
            {
                foreach (var item in entity.Rooms.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.Rooms.Clear();
            }

            base.Delete(entity);
        }

        public override void DeleteAll(IEnumerable<NegotiationConfig> entities)
        {
            foreach (var item in entities)
            {
                Delete(item);
            }
        }
    }
}
