﻿using PlataformaRio2C.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace PlataformaRio2C.Domain.Validation
{
    public class Validation<TEntity> : IValidation<TEntity>
         where TEntity : class
    {
        private readonly Dictionary<string, IValidationRule<TEntity>> _validationsRules;

        public Validation()
        {
            _validationsRules = new Dictionary<string, IValidationRule<TEntity>>();
        }

        protected virtual void AddRule(IValidationRule<TEntity> validationRule)
        {
            var ruleName = validationRule.GetType() + Guid.NewGuid().ToString("D");
            _validationsRules.Add(ruleName, validationRule);
        }

        protected virtual void RemoveRule(string ruleName)
        {
            _validationsRules.Remove(ruleName);
        }

        public virtual ValidationResult Valid(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var key in _validationsRules.Keys)
            {
                var rule = _validationsRules[key];
                if (!rule.Valid(entity))
                    result.Add(new ValidationError(rule.ErrorMessage, new string[] { rule.Target }, rule.Code));
            }
            return result;
        }
    }
}
