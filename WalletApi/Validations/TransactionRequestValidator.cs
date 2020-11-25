using ApplicationServices.Wallet.Queries;
using FluentValidation;

namespace WalletApi.Validations
{
    public class TransactionRequestValidator : NullValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required").NotNull()
               .WithMessage("PhoneNumber is required");
            RuleFor(model => model.AccountNumber).NotEmpty().WithMessage("AccountNumber is required").NotNull()
               .WithMessage("AccountNumber is required");
        }
    }
}
