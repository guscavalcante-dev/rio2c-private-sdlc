using System.Linq;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class RoomRepository : Repository<PlataformaRio2CContext, Room>, IRoomRepository
    {
        public RoomRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        #region Old Methods

        //public override IQueryable<Room> GetAll(bool @readonly = false)
        //{
        //    var consult = this.dbSet
        //                        .Include(i => i.Names)
        //                        .Include(i => i.Names.Select(e => e.Language));


        //    return @readonly
        //      ? consult.AsNoTracking()
        //      : consult;
        //}

        //public override IQueryable<Room> GetAllSimple()
        //{
        //    return this.dbSet
        //                        .Include(i => i.Names)
        //                        .Include(i => i.Names.Select(e => e.Language))
        //                        .AsNoTracking();
        //}

        //public override IQueryable<Room> GetAllSimple(Expression<Func<Room, bool>> filter)
        //{
        //    return GetAllSimple().Where(filter);
        //}

        //public override Room Get(Guid uid)
        //{
        //    return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        //}

        //public override Room Get(object id)
        //{
        //    return this.dbSet
        //                        .Include(i => i.Names)
        //                        .Include(i => i.Names.Select(e => e.Language))
        //        .FirstOrDefault(x => x.Id == (int)id);

        //}

        //public override void Delete(Room entity)
        //{
        //    if (entity.Names != null && entity.Names.Any())
        //    {
        //        foreach (var item in entity.Names.ToList())
        //        {
        //            _context.Entry(item).State = EntityState.Deleted;
        //        }

        //        entity.Names.Clear();
        //    }

        //    base.Delete(entity);
        //}

        #endregion
    }
}
