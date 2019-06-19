namespace PlataformaRio2C.Domain.Entities
{
    public class RoomName : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 256;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int RoomId { get; private set; }
        public virtual Room Room { get; private set; }      

        protected RoomName()
        {

        }

        public RoomName(string value, Language language, Room room)
        {
            Value = value;
            Language = language;
            LanguageId = language.Id;

            Room = room;
            RoomId = room.Id;
        }

        public RoomName(string value, string languageCode)
        {
            Value = value;
            LanguageCode = languageCode;
        }


        public void SetLanguage(Language language)
        {
            Language = language;
            LanguageId = language.Id;
        }
       

        public override bool IsValid()
        {
            return true;
        }
    }
}
