namespace Shared.Contracts.Validations
{
    public interface IAbstractValidator<T> where T : class
    {
        IReadOnlyCollection<Error> Errors { get; }
        bool IsValid { get; }
        bool IsFailure { get; }
        void RegisterRules();
        AbstractValidator<T> Validate(T model);
    }
}