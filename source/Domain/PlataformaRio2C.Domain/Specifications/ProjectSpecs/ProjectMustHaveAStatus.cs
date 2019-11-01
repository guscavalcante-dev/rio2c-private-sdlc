//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProjectMustHaveAStatus : ISpecification<Project>
//    {
//        public string Target { get { return "ProjectStatus"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Project entity)
//        {
//            return entity.Interests != null && entity.Interests.Any(i => i.Interest.InterestGroup.Name.Contains("Project Status"));
//        }
//    }
//}
