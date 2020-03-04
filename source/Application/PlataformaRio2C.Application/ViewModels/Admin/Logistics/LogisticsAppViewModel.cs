//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class LogisticsAppViewModel : EntityViewModel<LogisticsAppViewModel, Logistics>, IEntityViewModel<Logistics>
//    {
//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = false)]
//        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [Display(Name = "ArrivalDate", ResourceType = typeof(Labels))]
//        public DateTime? ArrivalDate { get; set; }

//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
//        [DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [Display(Name = "ArrivalTime", ResourceType = typeof(Labels))]
//        public TimeSpan? ArrivalTime { get; set; }
        
//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = false)]
//        [Display(Name = "DepartureDate", ResourceType = typeof(Labels))]
//        public DateTime? DepartureDate { get; set; }


//        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
//        [Display(Name = "DepartureTime", ResourceType = typeof(Labels))]
//        public TimeSpan? DepartureTime { get; set; }

//        [Display(Name = "Executive", ResourceType = typeof(Labels))]
//        public LogisticsCollaboratorAppViewModel Collaborator { get; set; }

//        [Display(Name = "OriginalName", ResourceType = typeof(Labels))]
//        public string OriginalName { get; set; }
//        [Display(Name = "ServerName", ResourceType = typeof(Labels))]
//        public string ServerName { get; set; }

//        public string TextEmail { get; set; }

//        public LogisticsAppViewModel()
//            :base()
//        {
//        }

//        public LogisticsAppViewModel(Logistics entity)
//            :base(entity)
//        {
//            ArrivalDate = entity.ArrivalDate;
//            ArrivalTime = entity.ArrivalTime;

//            DepartureDate = entity.DepartureDate;
//            DepartureTime = entity.DepartureTime;

//            OriginalName = entity.OriginalName;
//            ServerName = entity.ServerName;

//            SetCollaborator(entity.Collaborator);
//        }

//        public void SetCollaborator(Collaborator collaborator)
//        {
//            if (collaborator != null)
//            {
//                //var nameCollaborator = collaborator.Name;

//                //if (collaborator.Players != null && collaborator.Players.Any())
//                //{
//                //    nameCollaborator = string.Format("{0} - {1} - {2}", collaborator.Name, string.Join(",", collaborator.Players.Select(e => e.Name)), string.Join(",", collaborator.Players.Select(e => e.Holding.Name)));
//                //}
//                //else if (collaborator.ProducersEvents != null && collaborator.ProducersEvents.Any(e => !string.IsNullOrWhiteSpace(e.Producer.Name)))
//                //{
//                //    var namesProducers = collaborator.ProducersEvents.Select(e => e.Producer.Name).Where(e => !string.IsNullOrWhiteSpace(e)).Distinct();
//                //    nameCollaborator = string.Format("{0} - {1}", collaborator.Name, string.Join(",", namesProducers));
//                //}

//                //Collaborator = new LogisticsCollaboratorAppViewModel() { Name = nameCollaborator, Uid = collaborator.Uid };
//            }
            
//        }

//        public Logistics MapReverse()
//        {
//            var entity = new Logistics(ArrivalDate, DepartureDate);
//            entity.SetArrivalTime(ArrivalTime);
//            entity.SetDepartureTime(DepartureTime);

//            entity.SetOriginalName(OriginalName);
//            entity.SetServerName(ServerName);

//            return entity;
//        }

//        public Logistics MapReverse(Logistics entity)
//        {
//            entity.SetArrivalDate(ArrivalDate);
//            entity.SetArrivalTime(ArrivalTime);

//            entity.SetDepartureDate(DepartureDate);
//            entity.SetDepartureTime(DepartureTime);

//            entity.SetOriginalName(OriginalName);
//            entity.SetServerName(ServerName);

//            return entity;
//        }
//    }
//}
