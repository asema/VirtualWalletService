using System.Threading;
using System.Threading.Tasks;
using ApplicationServices.Utility;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ViewModel;

namespace ApplicationServices.Wallet.Commands
{
    public class CreateWalletRequestHandler : IRequestHandler<CreateWalletRequest, CreateWalletResponse>
    {
        private readonly CurrencyWalletContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWalletRequestHandler> _logger;

        public CreateWalletRequestHandler(CurrencyWalletContext context, IMapper mapper,
            ILogger<CreateWalletRequestHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CreateWalletResponse> Handle(CreateWalletRequest request, CancellationToken cancellationToken)
        {
            //generate account number - recursive method
            string accountNumber = await GetAccountNumber(cancellationToken);

            //save in database
            var newWallet = _mapper.Map<Domain.Wallet>(request);
            newWallet.AccountNumber = accountNumber;

            await _context.Wallets.AddAsync(newWallet, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var response = _mapper.Map<CreateWalletResponse>(newWallet);

            return response;
        }


        private async Task<string> GetAccountNumber(CancellationToken cancellationToken)
        {
            var accountNumber = RandomGenerator.GenerateAccountNumber(10);
            var result = await _context.Wallets.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber, cancellationToken);
            if (result != null)
            {
                accountNumber = await GetAccountNumber(cancellationToken);
            }
            return accountNumber;
        }
    }
}