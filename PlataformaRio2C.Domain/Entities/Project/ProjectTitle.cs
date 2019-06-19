namespace PlataformaRio2C.Domain.Entities
{
    public class ProjectTitle : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 256;

        public string Value { get; private set; }
        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        protected ProjectTitle()
        {

        }

        public ProjectTitle(string value, Language language, Project project)
        {
            Value = value;

            SetLanguage(language);
            SetProject(project);
        }
      

        public ProjectTitle(string value, string languageCode)
        {
            Value = value;
            LanguageCode = languageCode;
        }

        public void SetLanguage(Language language)
        {
            Language = language;
            if (Language != null)
            {
                LanguageId = language.Id;
            }
        }

        public void SetProject(Project project)
        {
            Project = project;

            if (Project != null)
            {
                ProjectId = project.Id;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
