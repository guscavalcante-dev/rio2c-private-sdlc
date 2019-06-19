namespace PlataformaRio2C.Domain.Entities
{
    public class ConferenceTitle : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 8000;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int ConferenceId { get; private set; }
        public virtual Conference Conference { get; private set; }

        protected ConferenceTitle()
        {

        }
     

        public ConferenceTitle(string value, Language language)
        {
            Value = value;
            SetLanguage(language);
        }

        public ConferenceTitle(string value, string languageCode)
        {
            Value = value;
            LanguageCode = languageCode;
        }

        public void SetLanguage(Language language)
        {
            Language = language;
            if (Language != null)
            {
                LanguageId = language.Id;
            }            
        }

        public void SetConference(Conference conference)
        {
            Conference = conference;

            if (conference != null)
            {
                ConferenceId = conference.Id;
            }            
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
