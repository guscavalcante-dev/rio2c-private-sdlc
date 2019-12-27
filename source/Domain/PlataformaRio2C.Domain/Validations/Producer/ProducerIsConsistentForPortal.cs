//using PlataformaRio2C.Domain.Entities.Specifications;
//using PlataformaRio2C.Domain.Validation;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System.Globalization;
//using System.Threading;

//namespace PlataformaRio2C.Domain.Entities.Validations
//{
//    public class ProducerIsConsistentForPortal : Validation<Producer>
//    {
//        public ProducerIsConsistentForPortal()
//        {
//            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

//            base.AddRule(new ValidationRule<Producer>(new ProducerMustHaveName(), Messages.NameIsRequired));
//            base.AddRule(new ValidationRule<Producer>(new ProducerDescriptionEnIsRequired(), Messages.CompanySummaryInEnglishMandatory));            
           
//            base.AddRule(new ValidationRule<Producer>(new ProducerTradeNameIsRequired(), Messages.TradeNameIsRequired));

//            if (currentCulture != null && currentCulture.Name == "pt-BR")
//            {
//                //base.AddRule(new ValidationRule<Producer>(new ProducerCnpjIsValid(), Messages.VatNumberIsInvalid));
//                base.AddRule(new ValidationRule<Producer>(new ProducerDescriptionPtBrIsRequired(), Messages.CompanySummaryInPtBrMandatory));
//            }
//        }
//    }
//}
