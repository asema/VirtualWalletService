using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace WalletApi.Validations
{
    public class NullValidator<T> : AbstractValidator<T>
    {
        public override Task<ValidationResult> ValidateAsync(ValidationContext<T> context, CancellationToken cancellation = new CancellationToken())
        {
            return context.InstanceToValidate == null
                ? Task.FromResult(new ValidationResult(new[] { new ValidationFailure("Request", "Request is empty", "Error") }))
                : base.ValidateAsync(context, cancellation);
        }
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            return context.InstanceToValidate == null
                ? new ValidationResult(new[] { new ValidationFailure("Request", "Request is empty", "Error") })
                : base.Validate(context);
        }
    }
}