using ApplicationServices.Exceptions;
using AutoMapper;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Wallet.Queries
{
    public class TransactionRequestHandler : IRequestHandler<TransactionRequest, List<TransactionResponse>>
    {
        private readonly CurrencyWalletContext _context;
        private readonly IMapper _mapper;

        public TransactionRequestHandler(CurrencyWalletContext  context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TransactionResponse>> Handle(TransactionRequest request, CancellationToken cancellationToken)
        {
            //get all the transactions belonging to the user
            Domain.Wallet wallet = await _context.Wallets.FirstOrDefaultAsync(a => a.PhoneNumber == request.PhoneNumber, cancellationToken);
            if (wallet != null)
            {
                if (wallet.AccountNumber != request.AccountNumber)
                {
                    throw new BadRequestException("Account Number or Phone Number is not correct");
                }
                else
                {
                    var transactions = await _context.Transactions.Where(a => a.WalletId == wallet.Id).ToListAsync();
                    var transactionResponse = _mapper.Map<List<TransactionResponse>>(transactions);
                    return transactionResponse;
                }
            }
            else
            {
                throw new BadRequestException("Account Number or Phone Number is not correct");
            }
        }
    }
}
