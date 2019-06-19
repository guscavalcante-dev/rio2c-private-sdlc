using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectLinkTeaserAppViewModel : EntityViewModel<ProjectLinkTeaserAppViewModel, ProjectLinkTeaser>, IEntityViewModel<ProjectLinkTeaser>
    {
        [AllowHtml]
        public string Value { get; set; }       
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectLinkTeaserAppViewModel()
        {

        }

        public ProjectLinkTeaserAppViewModel(Domain.Entities.ProjectLinkTeaser entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;
        }

        public ProjectLinkTeaser MapReverse()
        {
            var entity = new ProjectLinkTeaser(Value);

            return entity;
        }

        public void SetProject(ProjectBasicAppViewModel project)
        {
            Project = project;
        }

        public ProjectLinkTeaser MapReverse(ProjectLinkTeaser entity)
        {
            return entity;
        }
    }
}
