using PlataformaRio2C.Domain.Enums;
using PlataformaRio2C.Domain.Interfaces;

namespace PlataformaRio2C.Domain.Validation
{
    public class ValidationRule<TEntity> : IValidationRule<TEntity>
    {
        private readonly ISpecification<TEntity> _specificationRule;

        public ValidationRule(ISpecification<TEntity> specificationRule, string errorMessage)
        {
            _specificationRule = specificationRule;

            Target = _specificationRule.Target;
            Code = _specificationRule.Code;
            ErrorMessage = errorMessage;
        }

        public ErrorCodes Code { get; private set; }

        public string Target { get; private set; }
        public string ErrorMessage { get; private set; }

        public bool Valid(TEntity entity)
        {
            return _specificationRule.IsSatisfiedBy(entity);
        }
    }
}
