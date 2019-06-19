using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetAllByAdmin();
        IEnumerable<Project> GetAllExcel();
        IEnumerable<Project> GetDataExcel();
        int GetMaxNumberProjectPerProducer();
        int GetMaxNumberPlayerPerProject();
        string GetMaximumDateForEvaluation();

        Project GetSimpleWithProducer(Expression<Func<Project, bool>> filter);
        Project GetSimpleWithPlayers(Expression<Func<Project, bool>> filter);

        Project GetWithPlayerSelection(Guid uid);

        IQueryable<Project> GetAllOption(Expression<Func<Project, bool>> filter);

        int CountUnsent();
        int CountSent();
    }
}
