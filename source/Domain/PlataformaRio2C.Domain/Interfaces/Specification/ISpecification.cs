using PlataformaRio2C.Domain.Enums;

namespace PlataformaRio2C.Domain.Interfaces
{
    public interface ISpecification<T>
    {
        string Target { get; }
        ErrorCodes Code { get; }
        bool IsSatisfiedBy(T entity);
    }
}
