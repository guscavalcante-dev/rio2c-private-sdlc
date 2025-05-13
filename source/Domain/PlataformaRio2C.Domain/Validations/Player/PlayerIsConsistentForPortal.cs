//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Globalization;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class PlayerIsConsistentForPortal : Validation<Player>
//    {
//        public PlayerIsConsistentForPortal()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            base.AddRule(new ValidationRule<Player>(new PlayerMustHaveName(), Messages.NameIsRequired));
//            // base.AddRule(new ValidationRule<Player>(new PlayerCompanyNameIsRequired(), Messages.CompanyNameIsRequired));
//            base.AddRule(new ValidationRule<Player>(new PlayerDescriptionEnIsRequired(), Messages.CompanySummaryInEnglishMandatory));                        
//            base.AddRule(new ValidationRule<Player>(new PlayerTradeNameIsRequired(), Messages.TradeNameIsRequired));


//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                base.AddRule(new ValidationRule<Player>(new PlayerDescriptionPtBrIsRequired(), Messages.CompanySummaryInPtBrMandatory));
//                base.AddRule(new ValidationRule<Player>(new PlayerCnpjIsValid(), Messages.VatNumberIsRequired));
//            }
//        }
//    }
//}
