//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Domain.Interfaces;
//using PlataformaRio2C.Infra.Data.Context;
//using System;
//using System.Data.Entity;
//using System.Linq;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Infra.Data.Repository.Repositories
//{
//    public class ProjectPlayerRepository : Repository<PlataformaRio2CContext, ProjectPlayer>, IProjectPlayerRepository
//    {
//        public ProjectPlayerRepository(PlataformaRio2CContext context)
//            : base(context)
//        {
//        }

//        public override IQueryable<ProjectPlayer> GetAll(Expression<Func<ProjectPlayer, bool>> filter)
//        {
//            return this.dbSet
//                        .Include(i => i.Project)
//                        //.Include(i => i.Project.Producer)
//                        .Include(i => i.Project.Titles)
//                        .Include(i => i.Project.Titles.Select(s => s.Language))
//                        .Include(i => i.Project.Interests)
//                        .Include(i => i.Project.Interests.Select(e => e.Interest))
//                        .Include(i => i.Project.Interests.Select(e => e.Interest.InterestGroup))
//                        //.Include(i => i.Project.PlayersRelated)
//                        //.Include(i => i.Project.PlayersRelated.Select(e => e.Player))
//                        //.Include(i => i.Project.PlayersRelated.Select(e => e.Evaluation))
//                        //.Include(i => i.Project.PlayersRelated.Select(e => e.Evaluation.Status))
//                        .Where(filter);
//        }

//        public override void Delete(ProjectPlayer entity)
//        {
//            if (entity.Evaluation != null)
//            {
//                _context.Entry(entity.Evaluation).State = EntityState.Deleted;
//            }

//            base.Delete(entity);
//        }

//        public IQueryable<ProjectPlayer> GetAllForProccessSchedule()
//        {
//            return this.dbSet
//                       .Include(i => i.Project)
//                       .Include(i => i.Project.Titles)
//                       .Include(i => i.Project.Titles.Select(e => e.Language))
//                       //.Include(i => i.Project.Producer)
//                       //.Include(i => i.Project.Producer.EventsCollaborators)
//                       //.Include(i => i.Project.Producer.EventsCollaborators.Select(e => e.Collaborator))
//                       .Include(i => i.Player)
//                       //.Include(i => i.Player.Collaborators)
//                       .Include(i => i.Evaluation)
//                       .Include(i => i.Evaluation.Status)
//                       .Where(e => e.Evaluation.Status.Code == "Accepted");
//        }

//        public int CountUnsent()
//        {
//            return this.dbSet
//                       .Count(e => !e.Sent);
//        }

//        public int CountOnEvaluation()
//        {
//            return this.dbSet
//                      .Include(i => i.Evaluation)
//                      .Include(i => i.Evaluation.Status)
//                       .Count(e => e.Sent && (e.EvaluationId == null || e.Evaluation.Status.Code == "OnEvaluation"));
//        }

//        public int CountAccepted()
//        {
//            return this.dbSet
//                      .Include(i => i.Evaluation)
//                       .Include(i => i.Evaluation.Status)
//                       .Count(e => e.Sent && e.Evaluation.Status.Code == "Accepted");
//        }

//        public int CountRejected()
//        {
//            return this.dbSet
//                      .Include(i => i.Evaluation)
//                       .Include(i => i.Evaluation.Status)
//                       .Count(e => e.Sent && e.Evaluation.Status.Code == "Rejected");
//        }
//    }
//}
