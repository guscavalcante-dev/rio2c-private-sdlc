//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ProducerActivity : Entity
//    {
//        public int ProducerId { get; private set; }        
//        public virtual Producer Producer { get; private set; }

//        public int ActivityId { get; private set; }
//        public virtual Activity Activity { get; private set; }

//        protected ProducerActivity()
//        {
//        }

//        public ProducerActivity(Producer producer, Activity activity)
//        {
//            SetProducer(producer);
//            SetActivity(activity);
//        }

//        public void SetProducer(Producer producer)
//        {
//            Producer = producer;
//            if (producer != null)
//            {
//                ProducerId = producer.Id;                
//            }
//        }

//        public void SetActivity(Activity activity)
//        {
//            Activity = activity;
//            if (activity != null)
//            {
//                ActivityId = activity.Id;
//            }
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
