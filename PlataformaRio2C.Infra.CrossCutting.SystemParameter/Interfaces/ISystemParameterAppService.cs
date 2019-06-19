using PlataformaRio2C.Infra.CrossCutting.SystemParameter.Models;
using PlataformaRio2C.Infra.CrossCutting.SystemParameter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public interface ISystemParameterAppService
    {
        IEnumerable<SystemParameterAppViewModel> All(bool @readonly = false);
        void ReIndex();
        AppValidationResult UpdateAll(IEnumerable<SystemParameterAppViewModel> systemParameterAppViewModels);
        T Get<T>(string code);
    }
}
