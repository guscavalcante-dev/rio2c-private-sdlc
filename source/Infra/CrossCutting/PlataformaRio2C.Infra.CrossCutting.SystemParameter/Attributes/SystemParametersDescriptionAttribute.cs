using System;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class SystemParametersDescriptionAttribute : Attribute
    {
        private readonly int _code;
        private readonly string _description;
        private readonly Type _type;
        private readonly object _defaultValue;
        private readonly int _groupCode;
        private readonly int _languageCode;

        public SystemParametersDescriptionAttribute(int code, string description, Type type, int groupCode, int languageCode, object defaultValue)
        {
            _code = code;
            _description = description;
            _type = type;
            _defaultValue = defaultValue;
            _groupCode = groupCode;
            _languageCode = languageCode;
        }

        public string GetSqlDelete(string tableName)
        {
            return string.Format("DELETE FROM [dbo].[{0}] WHERE [Code] = {1} AND [LanguageCode] = {2}", tableName, _code, _languageCode);
        }

        public SystemParametersDescriptionDto GetDto()
        {
            return new SystemParametersDescriptionDto { Code = _code, DefaultValue = _defaultValue, Description = _description, GroupCode = _groupCode, LanguageCode = _languageCode, Type = _type };
        }
    }

    public class SystemParametersDescriptionDto
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public Type Type { get; set; }
        public object DefaultValue { get; set; }
        public int GroupCode { get; set; }
        public int LanguageCode { get; set; }
    }
}
