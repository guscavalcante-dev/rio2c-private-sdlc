using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels
{
    public class RoomAppViewModel : EntityViewModel<RoomAppViewModel, Room>, IEntityViewModel<Domain.Entities.Room>
    {
        public IEnumerable<RoomNameAppViewModel> Names { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Labels))]
        public string Name { get; set; }

        public RoomAppViewModel()
            :base()
        {

        }

        public RoomAppViewModel(Domain.Entities.Room entity)
            :base(entity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            Name = "";

            if (entity.Names != null && entity.Names.Any())
            {
                Names = RoomNameAppViewModel.MapList(entity.Names);

                if (currentCulture != null && currentCulture.Name == "pt-BR")
                {
                    Name = Names.Where(e => e.LanguageCode == "PtBr").Select(e => e.Value).FirstOrDefault();
                }
                else
                {
                    Name = Names.Where(e => e.LanguageCode == "En").Select(e => e.Value).FirstOrDefault();
                }
            }
        }

        public Room MapReverse()
        {
            return new Domain.Entities.Room(null);
        }

        public Room MapReverse(Room entity)
        {
            return entity;
        }
    }
}
