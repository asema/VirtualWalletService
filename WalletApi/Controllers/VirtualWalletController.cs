using ApplicationServices.Wallet.Commands;
using ApplicationServices.Wallet.Queries;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel;

namespace WalletApi.Controllers
{
    [Route("api/currency-wallet-service")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class VirtualWalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VirtualWalletController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Create a Wallet that returns an Account Number
        /// </summary>
        /// <param name="request">The Request to be created</param>
        /// <returns>Returns Ok with an Account Number</returns>
        [Route("createWallet")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWalletRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Deposit to the Virtual Wallet
        /// </summary>
        /// <param name="request">The Request object</param>
        /// <returns>Returns Ok with Account Balance</returns>
        [Route("deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Withdraw from the Virtual Wallet
        /// </summary>
        /// <param name="request">The Request object</param>
        /// <returns>Returns Ok with Account Balance</returns>
        [Route("withdraw")]
        [HttpPost]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        /// <summary>
        /// Get the Transactions
        /// </summary>
        /// <param name="phoneNumber">phone number of the Wallet owner</param>
        /// <param name="accountNumber">account number of the Wallet owner</param>
        /// <returns>Returns Ok with with the List of transactions done</returns>
        [Route("transactions/{phoneNumber}/{accountNumber}")]
        [HttpGet]
        public async Task<IActionResult> Transactions([FromRoute] string phoneNumber,[FromRoute] string accountNumber)
        {
            var result = await _mediator.Send(new TransactionRequest { AccountNumber = accountNumber, PhoneNumber = phoneNumber });
            return Ok(result);
        }
    }
}