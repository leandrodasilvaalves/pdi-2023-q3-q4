using System.Collections.ObjectModel;
using Shared.Contracts.Errors;

namespace Shared.Contracts.Validations
{
    public abstract class AbstractValidator<T> : IAbstractValidator<T> where T : class
    {
        private readonly List<IRule> _rules;
        private readonly List<IError> _errors;

        public AbstractValidator()
        {
            _rules = new();
            _errors = new();
            Errors = new ReadOnlyCollection<IError>(_errors);
            RegisterRules();
        }

        public IReadOnlyCollection<IError> Errors { get; }
        public bool IsValid => !IsFailure;
        public bool IsFailure => _errors?.Any() ?? false;

        protected abstract void RegisterRules();

        public virtual async Task<AbstractValidator<T>> Validate(T model)
        {
            ClearErrors();           
            await Parallel.ForEachAsync(_rules, async (rule, ct) =>
            {
                var castedRule = ((IRule<T>)rule);
                await castedRule.Apply(model);
                AddError(castedRule.Error);
            });
            return this;
        }

        protected void AddError(IError error)
        {
            if (error is not null)
            {
                _errors.Add(error);
            }
        }
        protected void AddErrors(IEnumerable<IError> erros)
        {
            if (erros.Any())
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