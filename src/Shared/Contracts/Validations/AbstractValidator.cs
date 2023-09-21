using System.Collections.ObjectModel;

namespace Shared.Contracts.Validations
{
    public abstract class AbstractValidator<T> : IAbstractValidator<T> where T : class
    {
        private readonly List<IRule> _rules;
        private readonly List<Error> _errors;

        public AbstractValidator()
        {
            _rules = new();
            _errors = new();
            Errors = new ReadOnlyCollection<Error>(_errors);
            RegisterRules();
        }

        public IReadOnlyCollection<Error> Errors { get; }
        public bool IsValid => !IsFailure;
        public bool IsFailure => _errors?.Any() ?? false;

        public abstract void RegisterRules();

        public virtual AbstractValidator<T> Validate(T model)
        {
            ClearErrors();
            foreach (var rule in _rules)
            {
                var castedRule = ((IRule<T>)rule);
                castedRule.Apply(model);
                AddError(castedRule.Error);
            }
            return this;
        }

        protected void AddError(Error error) 
        {
            if(error is not null)
            {
                _errors.Add(error);
            }
        }
        protected void AddErrors(IEnumerable<Error> erros)
        {
            if(erros.Any())
            {
                _errors.AddRange(erros);
            }
        }

        public void AddRule(IRule rule)
        {
            _rules.Add(rule);
        }

        protected void ClearErrors() => _errors.Clear();
    }
}