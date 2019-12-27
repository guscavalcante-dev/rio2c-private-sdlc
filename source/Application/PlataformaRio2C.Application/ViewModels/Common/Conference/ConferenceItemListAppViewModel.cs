//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ConferenceItemListAppViewModel : EntityViewModel<ConferenceItemListAppViewModel, Conference>
//    {
//        [Display(Name = "Title", ResourceType = typeof(Labels))]
//        public string Title { get; set; }

//        [Display(Name = "LecturerMemberOneOrMany", ResourceType = typeof(Labels))]
//        public string LecturersName { get; set; }

//        [Display(Name = "Date", ResourceType = typeof(Labels))]
//        public DateTime? Date { get; set; }

//        [Display(Name = "Room", ResourceType = typeof(Labels))]
//        public string Room { get; set; }

//        public ConferenceItemListAppViewModel()
//            : base()
//        {

//        }

//        public ConferenceItemListAppViewModel(Conference entity)
//            : base(entity)
//        {
//            Date = entity.Date;

//            if (entity.Titles != null && entity.Titles.Any())
//            {
//                var titlePt = entity.Titles.FirstOrDefault(e => e.Language.Code == "PtBr");
//                var titleEn = entity.Titles.FirstOrDefault(e => e.Language.Code == "En");

//                if (titlePt != null && !string.IsNullOrWhiteSpace(titlePt.Value) && titleEn != null && !string.IsNullOrWhiteSpace(titleEn.Value))
//                {
//                    Title = string.Format("{0} | {1}", titlePt.Value, titleEn.Value);
//                }
//                else if (titlePt != null && !string.IsNullOrWhiteSpace(titlePt.Value) && (titleEn == null || string.IsNullOrWhiteSpace(titleEn.Value)))
//                {
//                    Title = titlePt.Value;
//                }

//                else if (titleEn != null && !string.IsNullOrWhiteSpace(titleEn.Value) && (titlePt == null || string.IsNullOrWhiteSpace(titlePt.Value)))
//                {
//                    Title = titleEn.Value;
//                }
//            }

//            if (entity.Room != null)
//            {
//                Room = entity.Room.GetName();
//            }

//            if (entity.Lecturers != null && entity.Lecturers.Any())
//            {
//                List<string> namesLecturers = new List<string>();
//                var namesLecturersPreregister = entity.Lecturers.Where(e => e.Collaborator != null).Select(e => e.Collaborator.FirstName).ToList();

//                if (namesLecturersPreregister != null && namesLecturersPreregister.Any())
//                {
//                    namesLecturers.AddRange(namesLecturersPreregister);
//                }

//                var namesLecturersNotPreRegistred = entity.Lecturers.Where(e => e.Collaborator == null && e.Lecturer != null && !string.IsNullOrWhiteSpace(e.Lecturer.Name)).Select(e => e.Lecturer.Name).ToList();

//                if (namesLecturersNotPreRegistred != null && namesLecturersNotPreRegistred.Any())
//                {
//                    namesLecturers.AddRange(namesLecturersNotPreRegistred);
//                }

//                LecturersName = string.Join(", ", namesLecturers.OrderBy(e => e));
//            }
//        }
//    }
//}
