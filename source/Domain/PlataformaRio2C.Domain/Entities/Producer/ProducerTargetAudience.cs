//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ProducerTargetAudience : Entity
//    {
//        public int ProducerId { get; private set; }
//        public virtual Producer Producer { get; private set; }

//        public int TargetAudienceId { get; private set; }
//        public virtual TargetAudience TargetAudience { get; private set; }

//        protected ProducerTargetAudience()
//        {            
//        }

//        public ProducerTargetAudience(Producer producer, TargetAudience targetAudience)
//        {
//            SetProducer(producer);
//            SetTargetAudience(targetAudience);
//        }

//        public void SetProducer(Producer producer)
//        {
//            Producer = producer;
//            if (producer != null)
//            {
//                ProducerId = producer.Id;
//            }
//        }


//        public void SetTargetAudience(TargetAudience targetAudience)
//        {
//            TargetAudience = targetAudience;
//            if (targetAudience != null)
//            {
//                TargetAudienceId = targetAudience.Id;
//            }
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
