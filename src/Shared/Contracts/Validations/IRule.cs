namespace Shared.Contracts.Validations
{
    public interface IRule
    {
        bool IsValid { get; }
        bool IsFailure { get; }
        Error Error { get; }
    }


    public interface IRule<TObject> : IRule where TObject : class
    {
        void Apply(TObject instance);
    }
}