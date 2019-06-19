namespace PlataformaRio2C.Domain.Entities
{
    public class HoldingDescription : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 8000;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int HoldingId { get; private set; }
        public virtual Holding Holding { get; private set; }

        protected HoldingDescription()
        {

        }

        public HoldingDescription(string value, Language language)
        {
            Value = value;
            SetLanguage(language); 
        }

        public HoldingDescription(string value, string languageCode)
        {
            Value = value;
            LanguageCode = languageCode;
        }

        public void SetLanguage(Language language)
        {
            Language = language;
            LanguageId = language.Id;
        }


        public void SetHolding(Holding holding)
        {
            Holding = holding;
            HoldingId = holding.Id;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
