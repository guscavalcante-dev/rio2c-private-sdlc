using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SystemTextDescriptionAttribute : Attribute
    {
        private readonly int _groupCode;
        private readonly string _description;
        private readonly string _initialValue;
        private readonly int _languageCode;

        public SystemTextDescriptionAttribute(int groupCode, int languageCode, string description, string initialValue)
        {
            _groupCode = groupCode;
            _languageCode = languageCode;
            _initialValue = initialValue;
            _description = description;
        }

        public SystemTextDescriptionDto GetDto()
        {
            return new SystemTextDescriptionDto { GroupCode = _groupCode, Description = _description, InitialValue = _initialValue, LanguageCode = _languageCode };
        }
    }

    public class SystemTextDescriptionDto
    {
        public string Code { get; set; }
        public int GroupCode { get; set; }
        public string Description { get; set; }
        public string InitialValue { get; set; }
        public int LanguageCode { get; set; }
    }
}
