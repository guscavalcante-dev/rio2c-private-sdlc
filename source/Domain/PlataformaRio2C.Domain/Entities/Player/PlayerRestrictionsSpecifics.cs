using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Entities
{
    public class PlayerRestrictionsSpecifics : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 8000;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int PlayerId { get; private set; }
        public virtual Player Player { get; private set; }

        protected PlayerRestrictionsSpecifics()
        {

        }

        public PlayerRestrictionsSpecifics(string value, Language language, Player player)
        {
            Value = value;

            SetLanguage(language);
            SetPlayer(player);
        }

        public PlayerRestrictionsSpecifics(string value, Language language)
        {
            Value = value;
            SetLanguage(language);
        }

        public PlayerRestrictionsSpecifics(string value, string languageCode)
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

        public void SetPlayer(Player player)
        {
            Player = player;

            if (Player != null)
            {
                PlayerId = player.Id;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
