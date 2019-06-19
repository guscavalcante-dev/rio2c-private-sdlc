using PlataformaRio2C.Domain.Validation;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IValidation<in TEntity>
    {
        ValidationResult Valid(TEntity entity);
    }
}
