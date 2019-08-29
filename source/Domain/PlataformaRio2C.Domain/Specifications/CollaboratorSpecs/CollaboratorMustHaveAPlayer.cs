//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class CollaboratorMustHaveAPlayer : ISpecification<Collaborator>
//    {
//        public string Target { get { return "Players"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Collaborator entity)
//        {
//            return entity.Players != null && entity.Players.Any();
//        }
//    }
//}
