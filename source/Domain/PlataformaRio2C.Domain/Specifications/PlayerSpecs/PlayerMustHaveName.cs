//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class PlayerMustHaveName : ISpecification<Player>
//    {
//        public string Target { get { return "Name"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsInvalid; } }

//        public bool IsSatisfiedBy(Player entity)
//        {
//            return !string.IsNullOrWhiteSpace(entity.Name);
//        }
//    }
//}
