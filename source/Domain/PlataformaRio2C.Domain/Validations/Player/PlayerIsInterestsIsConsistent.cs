//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Globalization;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class PlayerIsInterestsIsConsistent : Validation<Player>
//    {
//        public PlayerIsInterestsIsConsistent()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            base.AddRule(new ValidationRule<Player>(new PlayerRestrictionsSpecificsEnIsRequired(), Messages.RestrictionsSpecificsInEnglishMandatory));

//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                base.AddRule(new ValidationRule<Player>(new PlayerRestrictionsSpecificsPtBrIsRequired(), Messages.RestrictionsSpecificsInPtBrMandatory));
//            }
//        }
//    }
//}
