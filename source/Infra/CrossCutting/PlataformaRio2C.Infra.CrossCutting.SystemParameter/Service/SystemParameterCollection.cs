//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
//{
//    public class SystemParameterCollection : ISystemParameterCollection
//    {
//        private readonly Dictionary<SystemParameterCodes, SystemParameter> _systemParameters = new Dictionary<SystemParameterCodes, SystemParameter>();

//        public SystemParameterCollection(ISystemParameterRepository repositoryFactory)
//        {
//            var systemParameters = repositoryFactory.GetAll(e => e.LanguageCode == LanguageCodes.PtBr); //TODO: parametrizar o idioma

//            foreach (var systemParameter in systemParameters)
//            {
//                this._systemParameters.Add(systemParameter.Code, systemParameter);
//            }
//        }

//        public SystemParameter this[SystemParameterCodes systemParameterCode]
//        {
//            get
//            {
//                if (!this._systemParameters.ContainsKey(systemParameterCode))
//                {
//                    throw new ArgumentOutOfRangeException(string.Format("O parametro {0} não foi encontrado no banco de dados.", systemParameterCode.ToString()));
//                }

//                return this._systemParameters[systemParameterCode];
//            }
//        }
//    }
//}
