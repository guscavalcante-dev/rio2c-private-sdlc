using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectProductionPlanAppViewModel : EntityViewModel<ProjectProductionPlanAppViewModel, ProjectProductionPlan>, IEntityViewModel<ProjectProductionPlan>
    {
        public static readonly int ValueMaxLength = ProjectProductionPlan.ValueMaxLength;

        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectProductionPlanAppViewModel()
        {

        }

        public ProjectProductionPlanAppViewModel(Domain.Entities.ProjectProductionPlan entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectProductionPlan MapReverse()
        {
            var entity = new ProjectProductionPlan(Value, LanguageCode);

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

        public ProjectProductionPlan MapReverse(ProjectProductionPlan entity)
        {
            return entity;
        }
    }
}
