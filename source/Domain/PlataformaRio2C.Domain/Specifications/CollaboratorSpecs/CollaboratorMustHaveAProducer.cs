//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class CollaboratorMustHaveAProducer : ISpecification<Collaborator>
//    {
//        public string Target { get { return "Producers"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Collaborator entity)
//        {
//            return entity.ProducersEvents != null && entity.ProducersEvents.Any();
//        }
//    }
//}
