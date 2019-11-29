//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;

//namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
//{
//    public interface ISystemParameterRepository
//    {
//        void Create(SystemParameter entity);
//        SystemParameter Get(Expression<Func<SystemParameter, bool>> filter);
//        SystemParameter Get(SystemParameterCodes code);
//        SystemParameter Get(SystemParameterCodes code, LanguageCodes languageCode);

//        System.Linq.IQueryable<SystemParameter> GetAll();
//        System.Linq.IQueryable<SystemParameter> GetAll(Expression<Func<SystemParameter, bool>> filter);
//        IQueryable<SystemParameter> GetAll(SystemParameterGroupCodes groupCode);
//        IQueryable<SystemParameter> GetAll(SystemParameterGroupCodes groupCode, LanguageCodes languageCode);

//        T Get<T>(SystemParameterCodes code);

//        void Update(SystemParameter entity);
//        void UpdateAll(IEnumerable<SystemParameter> entities);
//    }
//}
