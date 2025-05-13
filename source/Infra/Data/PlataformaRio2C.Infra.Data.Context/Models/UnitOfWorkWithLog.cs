// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-29-2019
// ***********************************************************************
// <copyright file="UnitOfWorkWithLog.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using PlataformaRio2C.Infra.CrossCutting.Tools.Enums;
using PlataformaRio2C.Infra.CrossCutting.Tools.Extensions;
using PlataformaRio2C.Infra.CrossCutting.Tools.Interfaces;
using PlataformaRio2C.Infra.Data.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PlataformaRio2C.Infra.Data.Context.Models
{
    /// <summary>UnitOfWorkWithLog</summary>
    public class UnitOfWorkWithLog<T> : IUnitOfWork
       where T : DbContext
    {
        private readonly T _context;
        private readonly ILogService _log;
        private bool _disposed = false;

        /// <summary>Initializes a new instance of the <see cref="UnitOfWorkWithLog{T}"/> class.</summary>
        /// <param name="context">The context.</param>
        /// <param name="log">The log.</param>
        public UnitOfWorkWithLog(T context, ILogService log)
        {
            _context = context;
            _log = log;
        }

        /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases unmanaged and - optionally - managed resources.</summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing && _context != null)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        /// <summary>Saves the changes.</summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                var errorMessage = e.GetInnerMessage();
                //var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                _log.Log(LogType.ERROR, string.Format("Error in Database commit - Error: {0}", errorMessage));
                throw new Exception(errorMessage);
                //return new SaveChangesResult(false) { ValidationResults = validationResults };
            }
        }

        /// <summary>Gets the name of the entity.</summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        private string GetEntityName(DbEntityEntry entry)
        {
            var index = entry.Entity.GetType().Name.IndexOf('_');
            return index == -1 ? entry.Entity.GetType().Name : entry.Entity.GetType().Name.Substring(0, index);
        }

        /// <summary>Begins the transaction.</summary>
        public void BeginTransaction()
        {
            _disposed = false;
        }
    }
}