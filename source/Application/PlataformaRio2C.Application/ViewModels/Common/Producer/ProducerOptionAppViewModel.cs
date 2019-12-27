//using PlataformaRio2C.Domain.Entities;
//using PlataformaRio2C.Infra.CrossCutting.Resources;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerOptionAppViewModel
//    {
//        public Guid Uid { get; set; }

//        [Display(Name = "Name", ResourceType = typeof(Labels))]
//        public string Name { get; set; }

//        public string HoldingName { get; set; }

//        public ProducerOptionAppViewModel()
//        {

//        }

//        public ProducerOptionAppViewModel(Producer entity)
//        {
//            Uid = entity.Uid;
//            Name = entity.Name;            
//        }

//        public static IEnumerable<ProducerOptionAppViewModel> MapList(IEnumerable<Producer> entities)
//        {
//            foreach (var entity in entities)
//            {
//                yield return new ProducerOptionAppViewModel(entity);
//            }
//        }
//    }
//}
