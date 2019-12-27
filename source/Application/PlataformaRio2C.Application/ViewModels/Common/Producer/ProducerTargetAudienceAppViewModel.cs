//using System;
//using PlataformaRio2C.Application.Common;
//using PlataformaRio2C.Domain.Entities;
//using System.Linq;
//using System.Collections.Generic;

//namespace PlataformaRio2C.Application.ViewModels
//{
//    public class ProducerTargetAudienceAppViewModel : EntityViewModel<ProducerTargetAudienceAppViewModel, ProducerTargetAudience>, IEntityViewModel<ProducerTargetAudience>
//    {
//        public int ProducerId { get; set; }
//        public ProducerAppViewModel Producer { get; set; }

//        public int TargetAudienceId { get; set; }
//        public string TargetAudienceName { get; set; }
//        public virtual TargetAudienceAppViewModel TargetAudience { get; set; }

//        public bool Selected { get; set; }

//        public ProducerTargetAudienceAppViewModel()
//        {

//        }

//        public ProducerTargetAudienceAppViewModel(ProducerTargetAudience entity)
//        {
//            ProducerId = entity.ProducerId;
//            TargetAudienceId = entity.TargetAudienceId;
//            TargetAudienceName = entity.TargetAudience.Name;

//            Selected = entity.Producer != null && entity.Producer.ProducerTargetAudience != null && entity.Producer.ProducerTargetAudience.Any(e => e.TargetAudience.Name == entity.TargetAudience.Name);
//        }

//        public static IEnumerable<ProducerTargetAudienceAppViewModel> MapList(IEnumerable<TargetAudience> activities, Producer producer)
//        {
//            foreach (var activity in activities)
//            {
//                yield return new ProducerTargetAudienceAppViewModel(new ProducerTargetAudience(producer, activity));
//            }
//        }

//        public ProducerTargetAudience MapReverse(Producer p, TargetAudience t)
//        {
//            var entity = new ProducerTargetAudience(p, t);
//            return entity;
//        }

//        public ProducerTargetAudience MapReverse()
//        {
//            var entity = new ProducerTargetAudience(Producer.MapReverse(), TargetAudience.MapReverse());
//            return entity;
//        }

//        public ProducerTargetAudience MapReverse(ProducerTargetAudience entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
