using ApplicationServices.Exceptions;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Wallet.Commands
{
    public class WithdrawRequestHandler : IRequestHandler<WithdrawRequest, WithdrawResponse>
    {
        private readonly CurrencyWalletContext _context;
        private readonly IMapper _mapper;

        public WithdrawRequestHandler(CurrencyWalletContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<WithdrawResponse> Handle(WithdrawRequest request, CancellationToken cancellationToken)
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
                    //withdrawal
                    var balance = wallet.Withdraw(request.Amount);
                    var transaction = _mapper.Map<Domain.Transaction>(request);
                    transaction.WalletId = wallet.Id;
                    transaction.TransactionType = Domain.TransactionType.Withdrawal;

                    //add transaction record
                    await _context.Transactions.AddAsync(transaction, cancellationToken);

                    //update wallet balance
                    _context.Wallets.Update(wallet);

                    await _context.SaveChangesAsync(cancellationToken);

                    var response = _mapper.Map<WithdrawResponse>(wallet);

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
