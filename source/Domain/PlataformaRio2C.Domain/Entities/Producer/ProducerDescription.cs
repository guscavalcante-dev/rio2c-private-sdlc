//namespace PlataformaRio2C.Domain.Entities
//{
//    public class ProducerDescription : Entity
//    {
//        public static readonly int ValueMinLength = 2;
//        public static readonly int ValueMaxLength = 8000;

//        public string Value { get; private set; }

//        public int LanguageId { get; private set; }
//        public virtual string LanguageCode { get; private set; }
//        public virtual Language Language { get; private set; }

//        public int ProducerId { get; private set; }
//        public virtual Producer Producer { get; private set; }

//        protected ProducerDescription()
//        {

//        }

//        public ProducerDescription(string value, Language language, Producer producer)
//        {
//            Value = value;

//            SetLanguage(language);
//            SetProducer(producer);
//        }

//        public ProducerDescription(string value, Language language)
//        {
//            Value = value;
//            SetLanguage(language);
//        }

//        public ProducerDescription(string value, string languageCode)
//        {
//            Value = value;
//            LanguageCode = languageCode;
//        }

//        public void SetLanguage(Language language)
//        {
//            Language = language;
//            if (Language != null)
//            {
//                LanguageId = language.Id;
//            }            
//        }

//        public void SetProducer(Producer producer)
//        {
//            Producer = producer;

//            if (Producer != null)
//            {
//                ProducerId = producer.Id;
//            }            
//        }

//        public override bool IsValid()
//        {
//            return true;
//        }
//    }
//}
