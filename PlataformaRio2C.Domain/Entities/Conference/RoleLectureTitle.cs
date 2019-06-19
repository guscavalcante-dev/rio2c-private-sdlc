using PlataformaRio2C.Domain.Entities.Validations;
using PlataformaRio2C.Domain.Validation;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Entities
{
    public class RoleLecturerTitle : Entity
    {
        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 256;

        public string Value { get; private set; }

        public int LanguageId { get; private set; }
        public virtual string LanguageCode { get; private set; }
        public virtual Language Language { get; private set; }

        public int RoleLecturerId { get; private set; }
        public virtual RoleLecturer RoleLecturer { get; private set; }      

        protected RoleLecturerTitle()
        {

        }

        public RoleLecturerTitle(string value, Language language, RoleLecturer roleLecturer)
        {
            Value = value;
            Language = language;
            LanguageId = language.Id;

            RoleLecturer = roleLecturer;
            RoleLecturerId = roleLecturer.Id;
        }

        public RoleLecturerTitle(string value, string languageCode)
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
