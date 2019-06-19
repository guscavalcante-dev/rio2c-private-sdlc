using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface IValidationRule<in TEntity>
    {
        string Target { get; }
        ErrorCodes Code { get; }
        string ErrorMessage { get; }
        bool Valid(TEntity entity);
    }
}
