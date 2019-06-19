using PlataformaRio2C.Domain.Entities;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Application.ViewModels.Api
{
    public class ConferenceItemListAppViewModel
    {
        public Guid Uid { get; set; }
        public string Title { get; set; }                   
        public string Room { get; set; }        
        public DateTime? Date { get; set; }
        public string StartTime{ get; set; }
        public string EndTime { get; set; }
        public DateTime CreationDate { get; set; }

        public ConferenceItemListAppViewModel()
        {

        }

        public ConferenceItemListAppViewModel(Conference entity)
        {
            Uid = entity.Uid;
            CreationDate = entity.CreationDate;

            Title = entity.GetTitle();           

            if (entity.Room != null)
            {
                Room = entity.Room.GetName();
            }

            Date = entity.Date;

            if (entity.StartTime != null)
            {
                StartTime = entity.StartTime.Value.ToString("hh':'mm");
            }

            if (entity.EndTime != null)
            {
                EndTime = entity.EndTime.Value.ToString("hh':'mm");
            }
        }

        public static IEnumerable<ConferenceItemListAppViewModel> MapList(IEnumerable<Conference> entities)
        {
            foreach (var entity in entities)
            {

                yield return new ConferenceItemListAppViewModel(entity);
            }
        }
    }
}
