using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System.Data.Entity;
using System.Linq;
using System;
using System.Collections.Generic;
using EFBulkInsert;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class NegotiationRepository : Repository<PlataformaRio2CContext, Negotiation>, INegotiationRepository
    {
        public NegotiationRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }


        public override IQueryable<Negotiation> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                //.Include(i => i.Player)
                                .Include(i => i.Project)
                                .Include(i => i.Project.ProjectTitles.Select(e => e.Language))
                                //.Include(i => i.Project.Producer)
                                .Include(i => i.Room)
                                .Include(i => i.Room.Names)
                                .Include(i => i.Room.Names.Select(e => e.Language));

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Negotiation> GetAllSimple()
        {
            return this.dbSet
                               //.Include(i => i.Player)
                               .Include(i => i.Project)
                               //.Include(i => i.Project.Producer)
                               .Include(i => i.Room)
                               .Include(i => i.Room.Names)
                               .Include(i => i.Room.Names.Select(e => e.Language))
                               .AsNoTracking();

        }


        //public IEnumerable<Player> GetAllPlayers()
        //{
        //    return this.dbSet
        //                       .Include(i => i.Player)
        //                       .Include(i => i.Player.Holding)
        //                       .AsNoTracking()
        //                       .ToList()
        //                       .Select(e => e.Player)
        //                       .GroupBy(e => e.Id)
        //                       .Select(e => e.First());

        //}

        //public IEnumerable<Producer> GetAllProducers()
        //{
        //    return null;
        //    //return this.dbSet
        //    //                   .Include(i => i.Project)
        //    //                   .Include(i => i.Project.Producer)
        //    //                   .AsNoTracking()
        //    //                   .ToList()
        //    //                   .Select(e => e.Project.Producer)
        //    //                   .GroupBy(e => e.Id)
        //    //                   .Select(e => e.First());
        //}

        public void Truncate()
        {
            _context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "TRUNCATE TABLE [dbo].[Negotiation]");
        }

        public override void CreateAll(IEnumerable<Negotiation> entities)
        {
            try
            {
                _context.BulkInsert(entities);
            }
            catch (Exception e)
            {
                throw;
            }

        }

        public IQueryable<Negotiation> GetAllBySchedule(Expression<Func<Negotiation, bool>> filter)
        {
            return this.dbSet
                               //.Include(i => i.Player)
                               //.Include(i => i.Player.Holding)
                               .Include(i => i.Project)
                               .Include(i => i.Project.ProjectTitles)
                               .Include(i => i.Project.ProjectTitles.Select(e => e.Language))
                               //.Include(i => i.Project.Producer)
                               .Include(i => i.Room)
                               .Include(i => i.Room.Names)
                               .Include(i => i.Room.Names.Select(e => e.Language))
                               .Where(filter);
        }       
    }
}
