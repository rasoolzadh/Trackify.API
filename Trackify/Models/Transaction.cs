// File: Trackify/Models/Transaction.cs

namespace Trackify.Models // ✅ This line is the fix.
{
    public class Transaction
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required string Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public required string Note { get; set; }
    }
}