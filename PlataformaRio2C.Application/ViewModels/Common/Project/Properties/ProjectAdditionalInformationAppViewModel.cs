using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using System.Web.Mvc;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProjectAdditionalInformationAppViewModel : EntityViewModel<ProjectAdditionalInformationAppViewModel, ProjectAdditionalInformation>, IEntityViewModel<ProjectAdditionalInformation>
    {
        public static readonly int ValueMaxLength = ProjectAdditionalInformation.ValueMaxLength;

        [AllowHtml]
        public string Value { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName{ get; set; }

        public  LanguageAppViewModel Language { get; set; }
        public  ProjectBasicAppViewModel Project { get; set; }

        public ProjectAdditionalInformationAppViewModel()
        {

        }

        public ProjectAdditionalInformationAppViewModel(Domain.Entities.ProjectAdditionalInformation entity)
        {
            CreationDate = entity.CreationDate;
            Uid = entity.Uid;
            Value = entity.Value;

            LanguageCode = entity.Language.Code;
            LanguageName = entity.Language.Name;
        }

        public ProjectAdditionalInformation MapReverse()
        {
            var entity = new ProjectAdditionalInformation(Value, LanguageCode);

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

        public ProjectAdditionalInformation MapReverse(ProjectAdditionalInformation entity)
        {
            return entity;
        }
    }
}
