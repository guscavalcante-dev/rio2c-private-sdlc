using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class PlayerRepository : Repository<PlataformaRio2CContext, Player>, IPlayerRepository
    {
        public PlayerRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }

        public override IQueryable<Player> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Holding)
                                .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Interests)
                                .Include(i => i.Interests.Select(e => e.Interest))
                                .Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(e => e.Language))
                                .Include(i => i.PlayerActivitys)
                                .Include(i => i.PlayerActivitys.Select(e => e.Activity))
                                .Include(i => i.PlayerTargetAudience)
                                .Include(i => i.PlayerTargetAudience.Select(e => e.TargetAudience))
                                //.Include(i => i.Collaborators)
                                .Include(i => i.RestrictionsSpecifics)
                                .Include(i => i.RestrictionsSpecifics.Select(e => e.Language));

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Player> GetAll(Expression<Func<Player, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }


        public override Player Get(Expression<Func<Player, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Player Get(Guid uid)
        {
            return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        }

        public override Player Get(object id)
        {
            return this.dbSet
                                .Include(i => i.Holding)
                                .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Interests)
                                .Include(i => i.Interests.Select(e => e.Interest))
                                .Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(e => e.Language))
                                .Include(i => i.PlayerActivitys)
                                .Include(i => i.PlayerActivitys.Select(e => e.Activity))
                                .Include(i => i.PlayerTargetAudience)
                                .Include(i => i.PlayerTargetAudience.Select(e => e.TargetAudience))
                                //.Include(i => i.Collaborators)
                                .Include(i => i.RestrictionsSpecifics)
                .SingleOrDefault(x => x.Id == (int)id);

        }

        public override void Delete(Player entity)
        {
            if (entity.Image != null)
            {
                _context.Entry(entity.Image).State = EntityState.Deleted;
            }

            if (entity.Descriptions != null && entity.Descriptions.Any())
            {
                foreach (var item in entity.Descriptions.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.Descriptions.Clear();
            }

            if (entity.Interests != null && entity.Interests.Any())
            {
                foreach (var item in entity.Interests.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.Interests.Clear();
            }

            if (entity.PlayerActivitys != null && entity.PlayerActivitys.Any())
            {
                foreach (var item in entity.PlayerActivitys.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.PlayerActivitys.Clear();
            }

            if (entity.PlayerTargetAudience != null && entity.PlayerTargetAudience.Any())
            {
                foreach (var item in entity.PlayerTargetAudience.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.PlayerTargetAudience.Clear();
            }

            if (entity.RestrictionsSpecifics != null && entity.RestrictionsSpecifics.Any())
            {
                foreach (var item in entity.RestrictionsSpecifics.ToList())
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }

                entity.RestrictionsSpecifics.Clear();
            }

            base.Delete(entity);
        }

        public IQueryable<Player> GetAllNoImageWithInterest(Expression<Func<Player, bool>> filter)
        {
            return this.dbSet
                               .Include(i => i.Address)
                               .Include(i => i.Holding)
                               .Include(i => i.Interests)
                               .Include(i => i.Interests.Select(e => e.Interest))
                               .Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                               .AsNoTracking()
                               .Where(filter);
        }

        public IQueryable<Player> GetAllNoImage(Expression<Func<Player, bool>> filter)
        {
            return this.dbSet
                               .Include(i => i.Holding)
                                .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(e => e.Language))
                                .Include(i => i.PlayerActivitys)
                                .Include(i => i.PlayerActivitys.Select(e => e.Activity))
                                .Include(i => i.PlayerTargetAudience)
                                .Include(i => i.PlayerTargetAudience.Select(e => e.TargetAudience))
                                .Include(i => i.RestrictionsSpecifics)
                                .Include(i => i.RestrictionsSpecifics.Select(e => e.Language))
                                .Include(i => i.Interests)
                                //.Include(i => i.Collaborators)
                               .Where(filter);
        }

        public override IQueryable<Player> GetAllSimple()
        {
            return this.dbSet
                               .Include(i => i.Holding)
                               .AsNoTracking();
        }






        public Player GetImage(Expression<Func<Player, bool>> filter)
        {
            return this.dbSet
                               .Include(i => i.Image)
                               .AsNoTracking()
                               .FirstOrDefault(filter);
        }

        public Player GetSimple(Expression<Func<Player, bool>> filter)
        {
            return this.dbSet
                               .FirstOrDefault(filter);
        }

        public IQueryable<Player> GetAllWithHoldingSimple()
        {
            return this.dbSet
                              .Include(i => i.Holding)
                              .AsNoTracking();
        }

        //public IQueryable<Collaborator> GetAllCollaborators(Expression<Func<Player, bool>> filter)
        //{
        //    return this.dbSet

        //                       //.Include(i => i.Collaborators)
        //                       //.Include(i => i.Collaborators.Select(c => c.User))
        //                       //.Where(filter)
        //                       //.SelectMany(e => e.Collaborators)
        //                       .AsNoTracking();

        //}


        public IQueryable<Player> GetAllWithAddress()
        {
            return this.dbSet
                               .Include(i => i.Address)
                               .AsNoTracking();

        }

        public IQueryable<Player> GetAllApi()
        {
            var fieldsParameter = "Holding.Name";
            var fields = fieldsParameter.Split(',');

            var t = this.dbSet.Include(i => i.Holding).ToList();

            IQueryable<Player> query = null;

            //query = this._context.Players;
            //foreach (var param in fields)
            //{
            //    query = query.Include(param);
            //}
            //var result = query.ToList();


            throw new NotImplementedException();
        }

        public IQueryable<Player> GetImageAll(Expression<Func<Player, bool>> filter)
        {
            return this.dbSet
                              .Include(i => i.Image)
                              .AsNoTracking()
                              .Where(filter);
        }

        public IQueryable<Player> GetImageAll()
        {
            return this.dbSet
                              .Include(i => i.Image)
                              .AsNoTracking();
        }

        public Player GetAllWithInterests(Guid uid)
        {
            return this.dbSet
                                .Include(i => i.Holding)
                                .Include(i => i.Image)
                                .Include(i => i.Address)
                                .Include(i => i.Interests)
                                .Include(i => i.Interests.Select(e => e.Interest))
                                .Include(i => i.Descriptions)
                                .Include(i => i.Descriptions.Select(e => e.Language))
                                .Include(i => i.PlayerActivitys)
                                .Include(i => i.PlayerActivitys.Select(e => e.Activity))
                                .Include(i => i.PlayerTargetAudience)
                                .Include(i => i.PlayerTargetAudience.Select(e => e.TargetAudience))
                                .Include(i => i.RestrictionsSpecifics)
                                .Include(i => i.RestrictionsSpecifics.Select(e => e.Language))
                                //.Include(i => i.Collaborators)
                                //.Include(i => i.Collaborators.Select(e => e.Address))
                                //.Include(i => i.Collaborators.Select(e => e.JobTitles))
                                //.Include(i => i.Collaborators.Select(e => e.JobTitles.Select(j => j.Language)))
                                .FirstOrDefault(e => e.Uid == uid);
        }
    }
}
