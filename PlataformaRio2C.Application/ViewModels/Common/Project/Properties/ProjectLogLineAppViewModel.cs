using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectLogLineAppViewModel : EntityViewModel<ProjectLogLineAppViewModel, ProjectLogLine>, IEntityViewModel<ProjectLogLine>
    {
        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectLogLineAppViewModel()
        {

        }

        public ProjectLogLineAppViewModel(Domain.Entities.ProjectLogLine entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectLogLine MapReverse()
        {
            var entity = new ProjectLogLine(Value, LanguageCode);

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

        public ProjectLogLine MapReverse(ProjectLogLine entity)
        {
            return entity;
        }
    }
}
