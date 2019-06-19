namespace PlataformaRio2C.Domain.Entities
{
    public class ProjectLinkImage : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 3000;

        public string Value { get; private set; }
        public int ProjectId { get; private set; }
        public virtual Project Project { get; private set; }

        protected ProjectLinkImage()
        {

        }

        public ProjectLinkImage(string value, Project project)
        {
            Value = value;
            SetProject(project);
        }


        public ProjectLinkImage(string value)
        {
            Value = value;
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
