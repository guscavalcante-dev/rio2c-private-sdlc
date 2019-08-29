using PlataformaRio2C.Application.Common;
using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels.Site
{
    public class ConferenceItemListAppViewModel : EntityViewModel<ViewModels.Site.ConferenceItemListAppViewModel, Conference>
    {
        [Display(Name = "Lecturer", ResourceType = typeof(Labels))]
        public IEnumerable<ConferenceLecturerItemListAppViewModel> Lecturers { get; set; }

        [Display(Name = "Title", ResourceType = typeof(Labels))]
        public string Title { get; set; }

        [Display(Name = "LecturerOneOrMany", ResourceType = typeof(Labels))]
        public string LecturersName { get; set; }

        [Display(Name = "Date", ResourceType = typeof(Labels))]
        public DateTime? Date { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "StartTime", ResourceType = typeof(Labels))]
        public TimeSpan? StartTime { get; set; }

        public string StartTimeText { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
        [Display(Name = "EndTime", ResourceType = typeof(Labels))]
        public TimeSpan? EndTime { get; set; }

        public string EndTimeText { get; set; }

        [Display(Name = "Room", ResourceType = typeof(Labels))]
        public string Room { get; set; }

        public ConferenceItemListAppViewModel()
            : base()
        {

        }

        public ConferenceItemListAppViewModel(Conference entity)
            : base(entity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            Date = entity.Date;            
            StartTime = entity.StartTime;
            if (entity.StartTime != null)
            {
                StartTimeText = entity.StartTime.Value.ToString("hh':'mm");
            }

            EndTime = entity.EndTime;
            if (entity.EndTime != null)
            {
                EndTimeText = entity.EndTime.Value.ToString("hh':'mm");
            }

            if (entity.Titles != null && entity.Titles.Any())
            {
                var titlePt = entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr");
                var titleEn = entity.Titles.FirstOrDefault(e => e.Language.Code == "En");

                if (currentCulture != null && currentCulture.Name == "pt-BR")
                {
                    Title = titlePt != null ? titlePt.Value : null;
                }
                else
                {
                    Title = titleEn != null ? titleEn.Value : null;
                }
            }

            if (entity.Room != null)
            {
                Room = entity.Room.GetName();
            }

            if (entity.Lecturers != null && entity.Lecturers.Any())
            {
                List<string> namesLecturers = new List<string>();
                var namesLecturersPreregister = entity.Lecturers.Where(e => e.Collaborator != null).Select(e => e.Collaborator.FirstName).ToList();

                if (namesLecturersPreregister != null && namesLecturersPreregister.Any())
                {
                    namesLecturers.AddRange(namesLecturersPreregister);
                }

                var namesLecturersNotPreRegistred = entity.Lecturers.Where(e => e.Collaborator == null && e.Lecturer != null && !string.IsNullOrWhiteSpace(e.Lecturer.Name)).Select(e => e.Lecturer.Name).ToList();

                if (namesLecturersNotPreRegistred != null && namesLecturersNotPreRegistred.Any())
                {
                    namesLecturers.AddRange(namesLecturersNotPreRegistred);
                }

                LecturersName = string.Join(", ", namesLecturers.OrderBy(e => e));

                Lecturers = ConferenceLecturerItemListAppViewModel.MapList(entity.Lecturers.Where(e => e.Collaborator != null && e.IsPreRegistered || !e.IsPreRegistered && e.Lecturer != null && !string.IsNullOrWhiteSpace(e.Lecturer.Name)));
            }           
        }
    }
}
