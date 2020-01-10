//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Linq;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerActivityAppViewModel : EntityViewModel<ProducerActivityAppViewModel, ProducerActivity>, IEntityViewModel<ProducerActivity>
//    {

//        public int ProducerId { get; set; }
//        public ProducerAppViewModel Producer { get; set; }

//        public int ActivityId { get; set; }
//        public string ActivityName { get; set; }
//        public virtual ActivityAppViewModel Activity { get;  set; }

//        public bool Selected { get; set; }

//        public ProducerActivityAppViewModel()
//        {

//        }

//        public ProducerActivityAppViewModel(ProducerActivity entity)
//        {
//            ProducerId = entity.ProducerId;
//            ActivityId = entity.ActivityId;

//            ActivityName = entity.Activity.Name;

//            Selected = entity.Producer != null && entity.Producer.ProducerActivitys != null && entity.Producer.ProducerActivitys.Any(e => e.Activity.Name == entity.Activity.Name);
//        }

//        public static IEnumerable<ProducerActivityAppViewModel> MapList(IEnumerable<Activity> activities, Producer producer)
//        {
//            foreach (var activity in activities)
//            {
//                yield return new ProducerActivityAppViewModel(new ProducerActivity(producer, activity));
//            }
//        }


//        public ProducerActivity MapReverse()
//        {
//            var entity = new ProducerActivity(Producer.MapReverse(), Activity.MapReverse());
//            return entity;
//        }

//        public ProducerActivity MapReverse(Producer p, Activity a)
//        {
//            var entity = new ProducerActivity(p, a);
//            return entity;
//        }

//        public ProducerActivity MapReverse(ProducerActivity entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
