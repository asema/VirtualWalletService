using ApplicationServices.Exceptions;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Wallet.Commands
{
    public class DepositRequestHandler : IRequestHandler<DepositRequest, DepositResponse>
    {
        private readonly CurrencyWalletContext _context;
        private readonly IMapper _mapper;

        public DepositRequestHandler(CurrencyWalletContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DepositResponse> Handle(DepositRequest request, CancellationToken cancellationToken)
        {
            Domain.Wallet wallet = await _context.Wallets.FirstOrDefaultAsync(a => a.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (wallet != null)
            {
                if (wallet.AccountNumber != request.AccountNumber)
                {
                    throw new BadRequestException("Account Number or Phone Number is not correct");
                }
                else
                {
                    //deposit
                    var balance = wallet.Deposit(request.Amount);
                    var transaction = _mapper.Map<Domain.Transaction>(request);
                    transaction.WalletId = wallet.Id;
                    transaction.TransactionType = Domain.TransactionType.Deposit;
                    //add transaction record
                    await _context.Transactions.AddAsync(transaction, cancellationToken);

                    //update wallet balance
                     _context.Wallets.Update(wallet);

                   await _context.SaveChangesAsync(cancellationToken);

                    var response = _mapper.Map<DepositResponse>(wallet);

                    return response;

                }
            }
            else
            {
                throw new BadRequestException("Account Number or Phone Number is not correct");
            }
        }
    }
}
