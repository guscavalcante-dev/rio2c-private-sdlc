//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.ComponentModel.DataAnnotations;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ManualNegotiationRegisterAppViewModel : EntityViewModel<NegotiationAppViewModel, Negotiation>, IEntityViewModel<Negotiation>
//    {        
//        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy", ApplyFormatInEditMode = false)]
//        //[DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        //[Display(Name = "Date", ResourceType = typeof(Labels))]
//        public DateTime? Date { get; set; }
//        public string DateFormat { get
//            {
//                if (Date != null)
//                {
//                    return Date.Value.ToString("dd/MM/yyyy");
//                }

//                return null;                
//            }
//        }

//        //[Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = false)]
//        //[DataType(DataType.DateTime, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "TheFieldIsRequired")]
//        //[Display(Name = "StarTime", ResourceType = typeof(Labels))]
//        public TimeSpan? StarTime { get; set; }

//        public string StarTimeFormat
//        {
//            get
//            {
//                if (StarTime != null)
//                {
//                    return StarTime.Value.ToString("hh':'mm");
//                }

//                return null;
//            }
//        }

//        [Display(Name = "Player", ResourceType = typeof(Labels))]
//        public Guid Player { get; set; }
//        public string PlayerName { get; set; }

//        [Display(Name = "Project", ResourceType = typeof(Labels))]
//        public Guid Project { get; set; }
//        public string ProjectName { get; set; }


//        public Guid Room { get; set; }
//        public string RoomName { get; set; }

//        public int Table { get; set; }


//        public ManualNegotiationRegisterAppViewModel()
//            :base()
//        {

//        }

//        public ManualNegotiationRegisterAppViewModel(Negotiation entity)
//            :base(entity)
//        {

//        }

//        public Negotiation MapReverse()
//        {
//            var entity = new Negotiation(project:null);

//            return entity;
//        }

//        public Negotiation MapReverse(Negotiation entity)
//        {
//            return entity;
//        }
//    }
//}
