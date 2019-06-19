using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Context.Models
{
    public class UnitOfWorkWithLog<T> : IUnitOfWork
       where T : DbContext
    {
        private readonly T _context;
        private readonly ILogService _log;
        private bool _disposed = false;
        public UnitOfWorkWithLog(T context, ILogService log)
        {
            _context = context;
            _log = log;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _context != null)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        public SaveChangesResult SaveChanges()
        {
            try
            {
                var result = _context.SaveChanges();
                return new SaveChangesResult(true);
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var erroMessages = new List<string>();

                foreach (var entityValidationErrors in ex.EntityValidationErrors.Select(e => new { e.Entry, e.ValidationErrors }))
                {
                    foreach (var validationErro in entityValidationErrors.ValidationErrors)
                    {
                        validationResults.Add(new System.ComponentModel.DataAnnotations.ValidationResult(validationErro.ErrorMessage, new[] { validationErro.PropertyName }));
                        erroMessages.Add(string.Format("Database error in entity: {0} - property: {1} - error: {2}", GetEntityName(entityValidationErrors.Entry), validationErro.PropertyName, validationErro.ErrorMessage));
                    }
                }

                _log.Log(LogType.ERROR, string.Join(" | ", erroMessages));

                return new SaveChangesResult(false) { ValidationResults = validationResults };
            }
            catch (Exception e)
            {
                var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var message = e.Message;
                _log.Log(LogType.ERROR, string.Format("Error in Database commit - Error: {0}", e.Message));
                return new SaveChangesResult(false) { ValidationResults = validationResults };
            }
        }

        private string GetEntityName(DbEntityEntry entry)
        {
            var index = entry.Entity.GetType().Name.IndexOf('_');
            return index == -1 ? entry.Entity.GetType().Name : entry.Entity.GetType().Name.Substring(0, index);
        }

        public void BeginTransaction()
        {
            _disposed = false;
        }
    }
}
