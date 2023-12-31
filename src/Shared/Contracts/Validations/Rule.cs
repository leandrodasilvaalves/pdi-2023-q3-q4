using Shared.Contracts.Errors;

namespace Shared.Contracts.Validations
{
    public abstract class Rule<TObject> : IRule<TObject> where TObject : class
    {
        public bool IsValid => !IsFailure;
        public bool IsFailure => Error != null;
        public Error Error { get; protected set; }
        public abstract Task Apply(TObject instance);
    }
}