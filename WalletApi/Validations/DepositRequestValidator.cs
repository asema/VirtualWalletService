using ApplicationServices.Wallet.Commands;
using FluentValidation;

namespace WalletApi.Validations
{
    public class DepositRequestValidator : NullValidator<DepositRequest>
    {
        public DepositRequestValidator()
        {
            RuleFor(model => model.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required").NotNull()
               .WithMessage("PhoneNumber is required");
            RuleFor(model => model.AccountNumber).NotEmpty().WithMessage("AccountNumber is required").NotNull()
               .WithMessage("AccountNumber is required");
            RuleFor(model => model.Narration).NotEmpty().WithMessage("Narration is required").NotNull()
               .WithMessage("Narration is required");
            RuleFor(model => model.Amount).GreaterThan(0).WithMessage("Amount to deposit cannot be 0");
        }
    }
}
