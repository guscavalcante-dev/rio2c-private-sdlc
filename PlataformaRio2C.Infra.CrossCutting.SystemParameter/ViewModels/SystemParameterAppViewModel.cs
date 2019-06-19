using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.ViewModels
{
    public class SystemParameterAppViewModel
    {
        public int Id { get; set; }
        public Guid Uid { get; set; }        
        public string Code { get; set; }
        public LanguageCodes LanguageCode { get; set; }
        public string GroupCode { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string SubCode { get; set; }
        public DateTime? DateChanges { get; set; }        
        public bool Visible { get; set; }

        public SystemParameterAppViewModel()
        {

        }

        public SystemParameterAppViewModel(SystemParameter systemParameter)
        {
            Code = systemParameter.Code.ToDescription();
            Uid = systemParameter.Uid;
            LanguageCode = systemParameter.LanguageCode;
            GroupCode = systemParameter.GroupCode.ToString();
            TypeName = systemParameter.TypeName;
            Description = systemParameter.Description;
            Value = systemParameter.GetValue<string>();
            SubCode = systemParameter.SubCode;
            DateChanges = systemParameter.DateChanges;
            Visible = true;
        }


        public static  IEnumerable<SystemParameterAppViewModel> MapList(IEnumerable<SystemParameter> entities)
        {
            foreach (var entity in entities)
            {                
                yield return new SystemParameterAppViewModel(entity);
            }
        }
    }
}
