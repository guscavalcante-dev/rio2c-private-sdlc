using System;

namespace PlataformaRio2C.Infra.CrossCutting.Tools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SystemMessageDescriptionAttribute : Attribute
    {
        private readonly int _groupCode;
        private readonly int _type;
        private readonly string _description;
        private readonly string _initialValue;
        private readonly int _languageCode;

        public SystemMessageDescriptionAttribute(int groupCode, int type, int languageCode, string description, string initialValue)
        {
            _groupCode = groupCode;
            _type = type;
            _languageCode = languageCode;
            _initialValue = initialValue;
            _description = description;
        }

        public SystemMessageDescriptionDto GetDto()
        {
            return new SystemMessageDescriptionDto { GroupCode = _groupCode, Type = _type, Description = _description, InitialValue = _initialValue, LanguageCode = _languageCode };
        }
    }

    public class SystemMessageDescriptionDto
    {
        public string Code { get; set; }
        public int GroupCode { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string InitialValue { get; set; }
        public int LanguageCode { get; set; }
    }
}
