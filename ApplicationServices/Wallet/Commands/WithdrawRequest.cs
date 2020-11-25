using MediatR;

namespace ApplicationServices.Wallet.Commands
{
    public class WithdrawRequest : IRequest<WithdrawResponse>
    {
        public string AccountNumber { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }
}
