//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class CollaboratorMustHaveName : ISpecification<Collaborator>
//    {
//        public string Target { get { return "Name"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Collaborator entity)
//        {
//            return !string.IsNullOrWhiteSpace(entity.Name);
//        }
//    }
//}
