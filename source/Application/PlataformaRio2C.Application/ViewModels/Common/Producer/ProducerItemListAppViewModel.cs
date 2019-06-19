using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ProducerItemListAppViewModel : EntityViewModel<ProducerItemListAppViewModel, Producer>
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        [Display(Name = "CNPJ", ResourceType = typeof(Labels))]
        public string CNPJ { get; set; }

        public ProducerItemListAppViewModel()
            :base()
        {

        }

        public ProducerItemListAppViewModel(Producer entity)
            :base(entity)
        {
            Name = entity.Name;
            CNPJ = entity.CNPJ;

            if (CNPJ != null)
            {
                CNPJ = Producer.GetLintCnpj(CNPJ);
            }
        }
    }
}
