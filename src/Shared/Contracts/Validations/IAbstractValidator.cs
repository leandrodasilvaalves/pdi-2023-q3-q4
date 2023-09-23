using Shared.Contracts.Errors;

namespace Shared.Contracts.Validations
{
    public interface IAbstractValidator<T> where T : class
    {
        IReadOnlyCollection<IError> Errors { get; }
        bool IsValid { get; }
        bool IsFailure { get; }
        Task<AbstractValidator<T>> Validate(T model);
    }
}