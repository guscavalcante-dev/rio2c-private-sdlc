using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.ComponentModel.DataAnnotations;

namespace PlataformaRio2C.Application.ViewModels
{
    public class HoldingItemListAppViewModel : EntityViewModel<HoldingItemListAppViewModel, Holding>
    {
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public HoldingItemListAppViewModel()
            :base()
        {

        }

        public HoldingItemListAppViewModel(Holding entity)
            :base(entity)
        {
            Name = entity.Name;
        }
    }
}
