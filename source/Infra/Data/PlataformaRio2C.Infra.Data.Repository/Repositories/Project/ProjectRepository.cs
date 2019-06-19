using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Domain.Interfaces;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.Data.Repository.Repositories
{
    public class ProjectRepository : Repository<Context.PlataformaRio2CContext, Project>, IProjectRepository
    {
        private readonly ISystemParameterRepository _systemParameterRepository;

        public ProjectRepository(Context.PlataformaRio2CContext context, ISystemParameterRepository systemParameterRepository)
            : base(context)
        {
            _systemParameterRepository = systemParameterRepository;
        }

        public IEnumerable<Project> GetAllByAdmin()
        {
            return this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(t => t.Language))
                                .Include(i => i.PlayersRelated)
                                .Include(i => i.PlayersRelated.Select(t => t.Player))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation.Status))
                                .Include(i => i.Producer)
                                .AsNoTracking();
        }



        public IEnumerable<Project> GetAllExcel()
        {
            return this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(t => t.Language))
                                .Include(i => i.LogLines)
                                .Include(i => i.LogLines.Select(t => t.Language))
                                .Include(i => i.Summaries)
                                .Include(i => i.Summaries.Select(t => t.Language))
                                .Include(i => i.ProductionPlans)
                                .Include(i => i.ProductionPlans.Select(t => t.Language))
                                .Include(i => i.Interests)
                                .Include(i => i.Interests.Select(e => e.Interest))
                                .Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                                .Include(i => i.LinksImage)
                                .Include(i => i.LinksTeaser)
                                .Include(i => i.AdditionalInformations)
                                .Include(i => i.AdditionalInformations.Select(t => t.Language))
                                .Include(i => i.PlayersRelated)
                                .Include(i => i.PlayersRelated.Select(t => t.Player))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation).Select(t => t.Status))
                                .Include(i => i.Producer)
                                .Include(i => i.Producer.EventsCollaborators)
                                .Include(i => i.Producer.EventsCollaborators.Select(t => t.Collaborator))
                                .Include(i => i.Producer.EventsCollaborators.Select(t => t.Collaborator).Select(t => t.User))
                                .AsNoTracking();
        }

        public IEnumerable<Project> GetDataExcel()
        {
            return this.dbSet
                                .Include(i => i.PlayersRelated)
                                .Include(i => i.PlayersRelated.Select(t => t.Player))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(t => t.Language))
                                .Include(i => i.Producer)
                                .AsNoTracking();
        }

        public override IQueryable<Project> GetAll(bool @readonly = false)
        {
            var consult = this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(t => t.Language))
                                .Include(i => i.LogLines)
                                .Include(i => i.LogLines.Select(t => t.Language))
                                .Include(i => i.Summaries)
                                .Include(i => i.Summaries.Select(t => t.Language))
                                .Include(i => i.ProductionPlans)
                                .Include(i => i.ProductionPlans.Select(t => t.Language))
                                .Include(i => i.Interests)
                                .Include(i => i.Interests.Select(e => e.Interest))
                                .Include(i => i.Interests.Select(e => e.Interest.InterestGroup))
                                .Include(i => i.LinksImage)
                                .Include(i => i.LinksTeaser)
                                .Include(i => i.AdditionalInformations)
                                .Include(i => i.AdditionalInformations.Select(t => t.Language))
                                .Include(i => i.PlayersRelated)
                                .Include(i => i.PlayersRelated.Select(t => t.Player))
                                .Include(i => i.PlayersRelated.Select(t => t.Evaluation))
                                .Include(i => i.Producer);

            return @readonly
              ? consult.AsNoTracking()
              : consult;
        }

        public override IQueryable<Project> GetAll(Expression<Func<Project, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public override Project Get(Expression<Func<Project, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public override Project Get(Guid uid)
        {
            return this.GetAll().FirstOrDefault(e => e.Uid == uid);
        }

        public int GetMaxNumberProjectPerProducer()
        {
            return _systemParameterRepository.Get<int>(SystemParameterCodes.ProjectsMaxNumberPerProducer);
        }

        public int GetMaxNumberPlayerPerProject()
        {
            return _systemParameterRepository.Get<int>(SystemParameterCodes.ProjectsMaxNumberPlayerPerProject);
        }

        public string GetMaximumDateForEvaluation()
        {
            return _systemParameterRepository.Get<string>(SystemParameterCodes.ProjectsMaximumDateForEvaluation);
        }

        public Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                        .Include(i => i.Producer)
                        .Include(i => i.Producer.Projects)
                        .Include(i => i.PlayersRelated)
                        .FirstOrDefault(filter);
        }

        public Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                        .Include(i => i.PlayersRelated)
                        .Include(i => i.PlayersRelated.Select(e => e.Player))
                        .FirstOrDefault(filter);
        }


        public override void Delete(Project entity)
        {
            if (entity.AdditionalInformations != null && entity.AdditionalInformations.Any())
            {
                var items = entity.AdditionalInformations.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.AdditionalInformations.Clear();

            if (entity.Interests != null && entity.Interests.Any())
            {
                var items = entity.Interests.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.Interests.Clear();

            if (entity.LinksImage != null && entity.LinksImage.Any())
            {
                var items = entity.LinksImage.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.LinksImage.Clear();

            if (entity.LinksTeaser != null && entity.LinksTeaser.Any())
            {
                var items = entity.LinksTeaser.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.LinksTeaser.Clear();

            if (entity.LogLines != null && entity.Summaries.Any())
            {
                var items = entity.LogLines.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.LogLines.Clear();


            if (entity.PlayersRelated != null && entity.PlayersRelated.Any())
            {
                var items = entity.PlayersRelated.ToList();
                foreach (var item in items)
                {
                    if (item.Evaluation != null)
                    {
                        _context.Entry(item.Evaluation).State = EntityState.Deleted;
                    }

                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.PlayersRelated.Clear();

            if (entity.ProductionPlans != null && entity.ProductionPlans.Any())
            {
                var items = entity.ProductionPlans.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.ProductionPlans.Clear();

            if (entity.Summaries != null && entity.Summaries.Any())
            {
                var items = entity.Summaries.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.Summaries.Clear();

            if (entity.Titles != null && entity.Titles.Any())
            {
                var items = entity.Titles.ToList();
                foreach (var item in items)
                {
                    _context.Entry(item).State = EntityState.Deleted;
                }
            }
            entity.Titles.Clear();

            base.Delete(entity);
        }

        public Project GetWithPlayerSelection(Guid uid)
        {
            return this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(t => t.Language))
                                .Include(i => i.PlayersRelated)
                                .Include(i => i.PlayersRelated.Select(e => e.Player))
                                .Include(i => i.PlayersRelated.Select(e => e.SavedUser))
                                .Include(i => i.PlayersRelated.Select(e => e.SendingUser))
                                .Include(i => i.PlayersRelated.Select(e => e.Evaluation))
                                .Include(i => i.PlayersRelated.Select(e => e.Evaluation.Status))
                                .FirstOrDefault(e => e.Uid == uid);
        }

        public IQueryable<Project> GetAllOption(Expression<Func<Project, bool>> filter)
        {
            return this.dbSet
                                .Include(i => i.Titles)
                                .Include(i => i.Titles.Select(e => e.Language))
                                .Include(i => i.Producer)
                                .Where(filter);

        }

        public int CountUnsent()
        {
            return this.dbSet
                                .Include(i => i.PlayersRelated)
                                .Count(e => !e.PlayersRelated.Any() || (e.PlayersRelated.Count(p => !p.Sent) == e.PlayersRelated.Count()));

        }

        public int CountSent()
        {
            return this.dbSet
                                .Include(i => i.PlayersRelated)
                                .Count(e => e.PlayersRelated.Any(p => p.Sent));

        }


    }
}
