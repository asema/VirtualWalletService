namespace ApplicationServices.Wallet.Queries
{
    public class TransactionResponse
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }
}
