using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectSummaryAppViewModel : EntityViewModel<ProjectSummaryAppViewModel, ProjectSummary>, IEntityViewModel<ProjectSummary>
    {
        public static readonly int ValueMaxLength = ProjectSummary.ValueMaxLength;

        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectSummaryAppViewModel()
        {

        }

        public ProjectSummaryAppViewModel(Domain.Entities.ProjectSummary entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectSummary MapReverse()
        {
            var entity = new ProjectSummary(Value, LanguageCode);

            return entity;
        }

        public void SetLanguage(LanguageAppViewModel language)
        {
            Language = language;
        }

        public void SetProject(ProjectBasicAppViewModel project)
        {
            Project = project;
        }

        public ProjectSummary MapReverse(ProjectSummary entity)
        {
            return entity;
        }
    }
}
