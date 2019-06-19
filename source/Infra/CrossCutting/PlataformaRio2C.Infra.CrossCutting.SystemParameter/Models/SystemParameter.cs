using PlataformaRio2C.Infra.CrossCutting.Tools.Attributes;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Model;
using System;
using System.Linq;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public partial class SystemParameter
    {
        public static readonly int DescriptionMinLength = 2;
        public static readonly int DescriptionMaxLength = 256;

        public static readonly int ValueMinLength = 2;
        public static readonly int ValueMaxLength = 1000;

        public int Id { get; set; }

        public Guid Uid { get; set; }

        public SystemParameter()
        {
            Uid = Guid.NewGuid();
        }

        public SystemParameter(SystemParameterCodes code, LanguageCodes languageCode,
                               SystemParameterGroupCodes groupCode,
                               string typeName, string description, string value)
        {
            Code = code;
            LanguageCode = languageCode;
            GroupCode = groupCode;
            TypeName = typeName;
            Description = description;
            SetValue(value);
            Uid = Guid.NewGuid();
        }

        public SystemParameter(SystemParametersDescriptionDto dto)
        {
            Code = (SystemParameterCodes)dto.Code;
            LanguageCode = (LanguageCodes)dto.LanguageCode;
            GroupCode = (SystemParameterGroupCodes)dto.GroupCode;
            TypeName = dto.Type.FullName;
            Description = dto.Description;
            SetValue(dto.DefaultValue.ToString());
            Uid = Guid.NewGuid();
        }



        public SystemParameterCodes Code { get; set; }

        public LanguageCodes LanguageCode { get; set; }

        public SystemParameterGroupCodes GroupCode { get; set; }

        public string TypeName { get; set; }

        public string Description { get; set; }

        public string Value { get; private set; }

        public string SubCode { get; set; }

        public DateTime? DateChanges { get; set; }

        [LogConfig(NoLog = true)]
        public void SetValue(string planText)
        {
            this.DateChanges = DateTime.Now;
            this.SubCode = PasswordHelper.GetNewRandomPassword(5, EspecialCharRequerid: false);
            Value = planText.AesEncrypt(ComposeSalt());
        }

        public dynamic GetValue()
        {
            var type = Type.GetType(this.TypeName, true);
            if (this.DateChanges.HasValue && !string.IsNullOrEmpty(this.SubCode))
            {
                var salt = ComposeSalt();
                return type.Cast(Convert.ChangeType(this.Value.AesDecrypt(salt), type));
            }

            return type.Cast(Convert.ChangeType(this.Value.AesDecrypt(), type));
        }

        public T GetValue<T>()
        {
            if (this.DateChanges.HasValue && !string.IsNullOrEmpty(this.SubCode))
            {
                var salt = ComposeSalt();
                return (T)Convert.ChangeType(this.Value.AesDecrypt(salt), typeof(T));
            }

            return (T)Convert.ChangeType(this.Value.AesDecrypt(), typeof(T));
        }

        public void UpdateFromDto(SystemParametersDescriptionDto dto, bool notUpdateValue = false)
        {
            if (dto != null)
            {
                Description = dto.Description;
                GroupCode = (SystemParameterGroupCodes)dto.GroupCode;
                //if (!notUpdateValue)
                //{
                //    SetValue(dto.DefaultValue.ToString());
                //}
            }
        }



        private string ComposeSalt()
        {
            var salt = this.Code.ToString().Substring(0, 5);
            salt += this.DateChanges.Value.ToOADate().ToString().ReverserStringWithLength(5);
            salt += this.SubCode;
            return salt;
        }
    }
}
