using System.Collections.Generic;

namespace WalletApi.Models.Error
{
    public class ErrorResponse
    {
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
