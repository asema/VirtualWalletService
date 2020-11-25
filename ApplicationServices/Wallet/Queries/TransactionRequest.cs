using MediatR;
using System.Collections.Generic;

namespace ApplicationServices.Wallet.Queries
{
    public class TransactionRequest: IRequest<List<TransactionResponse>>
    {
        public string AccountNumber { get; set; }
        public string PhoneNumber { get; set; }
    }
}
