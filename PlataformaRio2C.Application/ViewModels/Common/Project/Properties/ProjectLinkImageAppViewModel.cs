using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectLinkImageAppViewModel : EntityViewModel<ProjectLinkImageAppViewModel, ProjectLinkImage>, IEntityViewModel<ProjectLinkImage>
    {
        [AllowHtml]
        public string Value { get; set; }       
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectLinkImageAppViewModel()
        {

        }

        public ProjectLinkImageAppViewModel(Domain.Entities.ProjectLinkImage entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;
        }

        public ProjectLinkImage MapReverse()
        {
            var entity = new ProjectLinkImage(Value);

            return entity;
        }

        public void SetProject(ProjectBasicAppViewModel project)
        {
            Project = project;
        }

        public ProjectLinkImage MapReverse(ProjectLinkImage entity)
        {
            return entity;
        }
    }
}
