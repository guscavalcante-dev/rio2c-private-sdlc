//using PlataformaRio2C.Domain.Enums;
//using PlataformaRio2C.Domain.Interfaces;
//using System.Linq;

//namespace PlataformaRio2C.Domain.Entities.Specifications
//{
//    public class PlayerRestrictionsSpecificsEnIsRequired : ISpecification<Player>
//    {
//        public string Target { get { return "RestrictionsSpecifics"; } }
//        public ErrorCodes Code { get { return ErrorCodes.IsRequired; } }
//        public bool IsSatisfiedBy(Player entity)
//        {
//            return entity.RestrictionsSpecifics != null && entity.RestrictionsSpecifics.Any(e => e.LanguageCode == LanguageCodes.En.ToString() && !string.IsNullOrWhiteSpace(e.Value));
//        }
//    }
//}
