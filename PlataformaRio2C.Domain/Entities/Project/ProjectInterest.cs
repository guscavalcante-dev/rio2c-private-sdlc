using System;

namespace PlataformaRio2C.Domain.Entities
{
    public class ProjectInterest : Entity
    {
        public int ProjectId { get; private set; }
        public virtual Guid ProjectUid { get; private set; }
        public virtual Project Project { get; private set; }            
        public int InterestId { get; private set; }
        public virtual Guid InterestUid { get; private set; }
        public virtual Interest Interest { get; private set; }

        protected ProjectInterest()
        {

        }

        public ProjectInterest(Project project, Interest interest)
        {
            SetProject(project);
            SetInterest(interest);
        }

        public void SetProject(Project project)
        {
            Project = project;
            if (project != null)
            {
                ProjectId = project.Id;
                ProjectUid = project.Uid;
            }
        }

        public void SetInterest(Interest interest)
        {
            Interest = interest;
            if (interest != null)
            {
                InterestId = interest.Id;
                InterestUid = interest.Uid;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
