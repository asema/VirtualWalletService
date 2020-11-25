using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using ViewModel;

namespace WalletApi.Validations
{
    public class CreateWalletRequestValidator : NullValidator<CreateWalletRequest>
    {
        private readonly CurrencyWalletContext _context;

        public CreateWalletRequestValidator(CurrencyWalletContext context)
        {
            _context = context;
            RuleFor(model => model.FirstName).NotEmpty().WithMessage("FirstName is required").NotNull()
               .WithMessage("FirstName is required");
            RuleFor(model => model.LastName).NotEmpty().WithMessage("LastName is required").NotNull()
               .WithMessage("LastName is required");
            RuleFor(model => model.Address).NotEmpty().WithMessage("Address is required").NotNull()
               .WithMessage("Address is required");
            RuleFor(model => model.PhoneNumber).NotNull().WithMessage("Phone Number cannot be null")
                .NotEmpty().MustAsync(PhoneDoesNotExist)
                .WithMessage("Phone Number already exist");
        }

        private async Task<bool> PhoneDoesNotExist(string phone, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(a => a.PhoneNumber == phone, cancellationToken);
            return wallet == null;
        }
    }
}
