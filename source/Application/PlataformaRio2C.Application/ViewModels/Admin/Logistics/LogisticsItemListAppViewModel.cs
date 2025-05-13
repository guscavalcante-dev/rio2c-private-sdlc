//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;


//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class LogisticsItemListAppViewModel : EntityViewModel<LogisticsItemListAppViewModel, Logistics>
//    {
//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [Display(Name = "ArrivalDate", ResourceType = typeof(Labels))]
//        public DateTime? ArrivalDate { get; set; }

//        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
//        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [Display(Name = "ArrivalTime", ResourceType = typeof(Labels))]
//        public TimeSpan? ArrivalTime { get; set; }
//        [Display(Name = "ArrivalTime", ResourceType = typeof(Labels))]
//        public string ArrivalTimeText { get; set; }

//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
//        [Display(Name = "DepartureDate", ResourceType = typeof(Labels))]
//        public DateTime? DepartureDate { get; set; }

//        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
//        [Display(Name = "DepartureTime", ResourceType = typeof(Labels))]
//        public TimeSpan? DepartureTime { get; set; }
//        [Display(Name = "DepartureTime", ResourceType = typeof(Labels))]
//        public string DepartureTimeText { get; set; }

//        //[Display(Name = "Executive", ResourceType = typeof(Labels))]
//        //public LogisticsCollaboratorAppViewModel Collaborator { get; set; }

//        [Display(Name = "Executive", ResourceType = typeof(Labels))]
//        public string Collaborator { get; set; }

//        public LogisticsItemListAppViewModel()
//            :base()
//        {
//        }

//        public LogisticsItemListAppViewModel(Logistics entity)
//            :base(entity)
//        {
//            ArrivalDate = entity.ArrivalDate;
//            ArrivalTime = entity.ArrivalTime;
//            if (entity.ArrivalTime != null)
//            {
//                ArrivalTimeText = entity.ArrivalTime.Value.ToString("hh':'mm");
//            }

//            DepartureDate = entity.DepartureDate;
//            DepartureTime = entity.DepartureTime;
//            if (entity.DepartureTime != null)
//            {
//                DepartureTimeText = entity.DepartureTime.Value.ToString("hh':'mm");
//            }
//            SetCollaborator(entity.Collaborator);
//        }

//        public void SetCollaborator(Collaborator collaborator)
//        {
//            //if (collaborator != null)
//            //{
//            //    var nameCollaborator = collaborator.Name;

//            //    if (collaborator.Players != null && collaborator.Players.Any())
//            //    {
//            //        nameCollaborator = string.Format("{0} - {1} - {2}", collaborator.Name, string.Join(",", collaborator.Players.Select(e => e.Name)), string.Join(",", collaborator.Players.Select(e => e.Holding.Name)));
//            //    }
//            //    else if (collaborator.ProducersEvents != null && collaborator.ProducersEvents.Any(e => !string.IsNullOrWhiteSpace(e.Producer.Name)))
//            //    {
//            //        var namesProducers = collaborator.ProducersEvents.Select(e => e.Producer.Name).Where(e => !string.IsNullOrWhiteSpace(e)).Distinct();
//            //        nameCollaborator = string.Format("{0} - {1}", collaborator.Name, string.Join(",", namesProducers));
//            //    }

//            //    Collaborator = nameCollaborator;

//            //    //Collaborator = new LogisticsCollaboratorAppViewModel() { Name = nameCollaborator, Uid = collaborator.Uid };
//            //}            
//        }      
//    }
//}
