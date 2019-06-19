using PlataformaRio2C.Domain.Entities.Specifications;
using PlataformaRio2C.Domain.Validation;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Globalization;
using System.Threading;

namespace PlataformaRio2C.Domain.Entities.Validations
{
    public class ProducerIsConsistentForPreRegistration : Validation<Producer>
    {
        public ProducerIsConsistentForPreRegistration()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            if (currentCulture != null && currentCulture.Name == "pt-BR")
            {
                base.AddRule(new ValidationRule<Producer>(new ProducerCnpjIsValid(), Messages.VatNumberIsRequired));                
            }
        }
    }
}
