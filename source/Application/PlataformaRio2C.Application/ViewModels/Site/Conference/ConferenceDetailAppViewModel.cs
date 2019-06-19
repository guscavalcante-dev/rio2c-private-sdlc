using PlataformaRio2C.Domain.Entities;
using PlataformaRio2C.Infra.CrossCutting.Resources;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace PlataformaRio2C.Application.ViewModels
{
    public class ConferenceDetailAppViewModel : ConferenceAppViewModel
    {
        public string Title { get; set; }

        public string Synopsis { get; set; }

        [Display(Name = "Lecturer", ResourceType = typeof(Labels))]
        public IEnumerable<LecturerDetailAppViewModel> LecturersDetails { get; set; }

        public ConferenceDetailAppViewModel()
            : base()
        {

        }

        public ConferenceDetailAppViewModel(Conference entity)
            : base(entity)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;

            if (entity.Titles != null && entity.Titles.Any())
            {
                var titlePt = entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr");
                var titleEn = entity.Titles.FirstOrDefault(e => e.Language.Code == "En");

                if (currentCulture != null && currentCulture.Name == "pt-BR")
                {
                    Title = titlePt.Value;
                }
                else
                {
                    Title = titleEn.Value;
                }
            }

            if (entity.Synopses != null && entity.Synopses.Any())
            {
                var synopsisPt = entity.Synopses.FirstOrDefault(e => e.Language.Code == "PtBr");
                var synopsisEn = entity.Synopses.FirstOrDefault(e => e.Language.Code == "En");

                if (currentCulture != null && currentCulture.Name == "pt-BR")
                {
                    Synopsis = synopsisPt.Value;
                }
                else
                {
                    Synopsis = synopsisEn.Value;
                }
            }

            if (entity.Lecturers != null && entity.Lecturers.Any(e => e.Collaborator != null && e.IsPreRegistered || !e.IsPreRegistered && !string.IsNullOrWhiteSpace(e.Lecturer.Name)))
            {
                LecturersDetails = LecturerDetailAppViewModel.MapList(entity.Lecturers.Where(e => e.Collaborator != null && e.IsPreRegistered || !e.IsPreRegistered && !string.IsNullOrWhiteSpace(e.Lecturer.Name)));
            }
        }


    }
}
