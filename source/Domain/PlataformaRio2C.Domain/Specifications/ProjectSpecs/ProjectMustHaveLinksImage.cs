//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class ProjectMustHaveLinksImage : ISpecification<Project>
//    {
//        public string Target { get { return "LinksImage"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Project entity)
//        {
//            return entity.LinksImage != null && entity.LinksImage.Any(e => !string.IsNullOrWhiteSpace(e.Value));
//        }
//    }
//}
