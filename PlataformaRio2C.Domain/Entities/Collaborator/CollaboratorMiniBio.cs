namespace PlataformaRio2C.Domain.Entities
{
    public class CollaboratorMiniBio : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 8000;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int CollaboratoId { get; private set; }
        public virtual Collaborator Collaborator { get; private set; }

        protected CollaboratorMiniBio()
        {

        }

        public CollaboratorMiniBio(string value, Language language, Collaborator collaborator)
        {
            Value = value;
            Language = language;
            LanguageId = language.Id;

            Collaborator = collaborator;
            CollaboratoId = collaborator.Id;
        }

        public CollaboratorMiniBio(string value, string languageCode)
        {
            Value = value;
            LanguageCode = languageCode;
        }

        public void SetLanguage(Language language)
        {
            Language = language;
            LanguageId = language.Id;
        }

        public void SetCollaborator(Collaborator collaborator)
        {
            Collaborator = collaborator;
            if (collaborator != null)
            {
                CollaboratoId = collaborator.Id;
            }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
