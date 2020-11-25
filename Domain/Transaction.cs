using System.Globalization;

namespace Domain
{
    public class Transaction : BaseEntity
    {
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }

    public enum TransactionType
    {
        Deposit=1, Withdrawal, Transfer
    }
}