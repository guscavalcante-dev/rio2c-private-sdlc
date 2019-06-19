using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public class SystemParameterRepository : ISystemParameterRepository
    {
        private readonly ILogService _logger;

        protected PlataformaRio2C.Infra.CrossCutting.SystemParameter.PlataformaRio2CContext _context;
        protected DbSet<SystemParameter> dbSet;

        public SystemParameterRepository(PlataformaRio2C.Infra.CrossCutting.SystemParameter.PlataformaRio2CContext context, ILogService logger)            
        {
            _logger = logger;
            _context = context;
            dbSet = context.Set<SystemParameter>();
        }

        public virtual void Create(SystemParameter entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual SystemParameter Get(Expression<Func<SystemParameter, bool>> filter)
        {
            return this.GetAll().FirstOrDefault(filter);
        }

        public void Delete(SystemParameter entity)
        {
            throw new InvalidOperationException("Não é possível deletar um parametro do sistema!");
        }

        public virtual IQueryable<SystemParameter> GetAll()
        {
            return this.dbSet;
        }

        public virtual IQueryable<SystemParameter> GetAll(Expression<Func<SystemParameter, bool>> filter)
        {
            return this.GetAll().Where(filter);
        }

        public void DeleteAll(IEnumerable<SystemParameter> entities)
        {
            throw new InvalidOperationException("Não é possível deletar um parametro do sistema!");
        }      

        public void CreateAll(IEnumerable<SystemParameter> entities)
        {
            throw new InvalidOperationException("Não é possível adicionar um parametro do sistema!");
        }

        public SystemParameter Get(SystemParameterCodes code)
        {
            return Get(code, LanguageCodes.PtBr);
        }

        public SystemParameter Get(SystemParameterCodes code, LanguageCodes languageCode)
        {
            return this.GetAll(e => e.Code == code && e.LanguageCode == languageCode).FirstOrDefault();
        }

        public IQueryable<SystemParameter> GetAll(SystemParameterGroupCodes groupCode)
        {
            return GetAll(groupCode, LanguageCodes.PtBr);
        }

        public IQueryable<SystemParameter> GetAll(SystemParameterGroupCodes groupCode, LanguageCodes languageCode)
        {
            return this.GetAll(e => e.GroupCode == groupCode && e.LanguageCode == languageCode);
        }

        public virtual void Update(SystemParameter entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateAll(IEnumerable<SystemParameter> entities)
        {
            foreach (var entity in entities)
            {
                this.Update(entity);
            }
        }

        public T Get<T>(SystemParameterCodes code)
        {
            try
            {
                return Get(code).GetValue<T>();
            }
            catch (Exception e)
            {
                _logger.Log(LogType.ERROR, string.Format("Parametro não encontrado no Banco de dados. {0}", e.Message));              
                return (T)default(SystemParameterCodes).SystemParametersDescriptions().Where(w => w.Code == (int)code).Select(s => s.DefaultValue).FirstOrDefault();
            }
        }
    }
}
