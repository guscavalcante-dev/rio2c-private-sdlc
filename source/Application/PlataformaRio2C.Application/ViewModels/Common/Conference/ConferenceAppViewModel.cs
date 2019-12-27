//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace PlataformaRio2C.Application.ViewModels 
//{
//    public class ConferenceAppViewModel : EntityViewModel<ConferenceAppViewModel, Conference>, IEntityViewModel<Conference>
//    {
//        [Display(Name = "Room", ResourceType = typeof(Labels))]
//        public Guid Room { get; set; }

//        [Display(Name = "Room", ResourceType = typeof(Labels))]
//        public string RoomName { get; set; }

//        [Display(Name = "Lecturer", ResourceType = typeof(Labels))]
//        public IEnumerable<LecturerAppViewModel> Lecturers { get; set; }

//        [Display(Name = "Title", ResourceType = typeof(Labels))]
//        public IEnumerable<ConferenceTitleAppViewModel> Titles { get; set; }

//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = false)]
//        [Display(Name = "Date", ResourceType = typeof(Labels))]
//        public DateTime? Date { get; set; }

//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [Display(Name = "StartTime", ResourceType = typeof(Labels))]
//        public TimeSpan? StartTime { get; set; }

//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]        
//        [Display(Name = "EndTime", ResourceType = typeof(Labels))]
//        public TimeSpan? EndTime { get; set; }



//        [Display(Name = "Synopsis", ResourceType = typeof(Labels))]
//        public IEnumerable<ConferenceSynopsisAppViewModel> Synopses { get; set; }

      

//        [AllowHtml]
//        [Display(Name = "AdditionalInformations", ResourceType = typeof(Labels))]
//        public string Info { get; set; }

//        public ConferenceAppViewModel()             
//            :base()  
//        {

//        }

//        public ConferenceAppViewModel(Conference entity)
//            :base(entity)
//        {            
//            Date = entity.Date;
//            Info = entity.Info;

//            StartTime = entity.StartTime;
//            EndTime = entity.EndTime;

//            if (entity.Room != null)
//            {
//                Room = entity.Room.Uid;
//                RoomName = entity.Room.GetName();
//            }

//            if (entity.Lecturers != null && entity.Lecturers.Any(e => e.Collaborator != null && e.IsPreRegistered || !e.IsPreRegistered && !string.IsNullOrWhiteSpace(e.Lecturer.Name)))
//            {
//                Lecturers = LecturerAppViewModel.MapList(entity.Lecturers.Where(e => e.Collaborator != null && e.IsPreRegistered || !e.IsPreRegistered && e.Lecturer != null && !string.IsNullOrWhiteSpace(e.Lecturer.Name)));
//            }

//            if (entity.Titles != null && entity.Titles.Any())
//            {
//                Titles = ConferenceTitleAppViewModel.MapList(entity.Titles);
//            }

//            if (entity.Synopses != null && entity.Synopses.Any())
//            {
//                Synopses = ConferenceSynopsisAppViewModel.MapList(entity.Synopses);
//            }
//        }

//        public Conference MapReverse()
//        {
//            var entity = new Conference(Date);
//            entity.SetInfo(Info);
//            entity.SetStartTime(StartTime);
//            entity.SetEndTime(EndTime);

//            return entity;
//        }

//        public Conference MapReverse(Conference entity)
//        {
//            entity.SetDate(Date);
//            entity.SetStartTime(StartTime);
//            entity.SetEndTime(EndTime);            
//            entity.SetInfo(Info);

//            return entity;
//        }
//    }
//}
