using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.Data.Context;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ConferenceRepository : Repository<PlataformaRio2CContext, Conference>, IConferenceRepository
    {
        public ConferenceRepository(PlataformaRio2CContext context)
            : base(context)
        {
        }


        public override IQueryable<Conference> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Room)
                                .Include(i => i.Room.Names)
                                .Include(i => i.Room.Names.Select(e => e.Language))
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(e => e.Language))
                                .Include(i => i.Synopses)
                                .Include(i => i.Synopses.Select(e => e.Language));
                                //.Include(i => i.Lecturers)
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator));
                                

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public IQueryable<Conference> GetAllBySchedule()
        {
            return this.dbSet
                                .Include(i => i.Room)
                                //.Include(i => i.Lecturers)
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.Players))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents.Select(pe => pe.Producer)))
                                .AsNoTracking();
        }

        public IQueryable<Conference> GetAllBySchedule(Expression<Func<Conference, bool>> filter)
        {
            return this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(e => e.Language))
                                .Include(i => i.Room)
                                .Include(i => i.Room.Names)
                                .Include(i => i.Room.Names.Select(e => e.Language))
                                //.Include(i => i.Lecturers)
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer))
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer.Titles))
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer.Titles.Select(rl => rl.Language)))
                                //.Include(i => i.Lecturers.Select(l => l.Lecturer))
                                //.Include(i => i.Lecturers.Select(l => l.Lecturer.JobTitles))
                                //.Include(i => i.Lecturers.Select(l => l.Lecturer.JobTitles.Select(j => j.Language)))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.Players))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.Players.Select(pl => pl.Holding)))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents.Select(pe => pe.Producer)))
                                .AsNoTracking()
                                .Where(filter);
        }

        public override Conference Get(Guid uid)
        {
            var consult = this.dbSet
                                .Include(i => i.Room)
                                .Include(i => i.Room.Names)
                                .Include(i => i.Room.Names.Select(e => e.Language))
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(e => e.Language))
                                .Include(i => i.Synopses)
                                .Include(i => i.Synopses.Select(e => e.Language));
                                //.Include(i => i.Lecturers)
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer))
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer.Titles))
                                //.Include(i => i.Lecturers.Select(e => e.RoleLecturer.Titles.Select(t => t.Language)))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.Players))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.Players.Select(p => p.Holding)))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents))
                                //.Include(i => i.Lecturers.Select(e => e.Collaborator.ProducersEvents.Select(p => p.Producer)))
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer))
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer))
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer.Image))
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer.JobTitles))
                                //.Include(i => i.Lecturers.Select(e => e.Lecturer.JobTitles.Select(j => j.Language)));


            return consult.FirstOrDefault( e => e.Uid == uid);
        }

        public override void Delete(Conference entity)
        {
            //if (entity.Lecturers != null && entity.Lecturers.Any())
            //{
            //    var items = entity.Lecturers.ToList();
            //    foreach (var item in items)
            //    {
            //        if (item.Lecturer != null)
            //        {
            //            if (item.Lecturer.JobTitles != null && item.Lecturer.JobTitles.Any())
            //            {
            //                var jobtitles = item.Lecturer.JobTitles.ToList();

            //                foreach (var jt in jobtitles)
            //                {
            //                    _context.Entry(jt).State = EntityState.Deleted;
            //                }
            //            }

            //            _context.Entry(item.Lecturer).State = EntityState.Deleted;
            //        }

            //        _context.Entry(item).State = EntityState.Deleted;
            //    }
            //}

            if (entity.Titles != null && entity.Titles.Any())
            {
                var items = entity.Titles.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }

            if (entity.Synopses != null && entity.Synopses.Any())
            {
                var items = entity.Synopses.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }

            base.Delete(entity);
        }        
    }
}
