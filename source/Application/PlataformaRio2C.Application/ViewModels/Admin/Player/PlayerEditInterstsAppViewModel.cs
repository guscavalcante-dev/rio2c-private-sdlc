using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Application.ViewModels
{
    public class PlayerEditInterstsAppViewModel
    {
        public Guid Uid { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public IEnumerable<LanguageAppViewModel> LanguagesOptions { get; set; }

        public IEnumerable<PlayerInterestAppViewModel> Interests { get; set; }

        [Display(Name = "RestrictionsSpecifics", ResourceType = typeof(Labels))]
        public IEnumerable<PlayerRestrictionsSpecificsAppViewModel> RestrictionsSpecifics { get; set; }

        public PlayerEditInterstsAppViewModel()
        {

        }

        public PlayerEditInterstsAppViewModel(Player entity)
        {
            Uid = entity.Uid;
            Name = entity.Name;

            if (entity.RestrictionsSpecifics != null && entity.RestrictionsSpecifics.Any())
            {
                RestrictionsSpecifics = PlayerRestrictionsSpecificsAppViewModel.MapList(entity.RestrictionsSpecifics).ToList();
            }
        }


    }
}
