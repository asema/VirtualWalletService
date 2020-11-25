using ApplicationServices.Wallet.Commands;
using MediatR;

namespace ViewModel
{
    public class CreateWalletRequest: IRequest<CreateWalletResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
