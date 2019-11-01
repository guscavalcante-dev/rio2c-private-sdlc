//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProjectMustHaveValueAlreadyRaised : ISpecification<Project>
//    {
//        public string Target { get { return "ValueAlreadyRaised"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Project entity)
//        {
//            return !string.IsNullOrWhiteSpace(entity.ValueAlreadyRaised);
//        }
//    }
//}
