namespace MobileRecharge.Core.Services
{
    public class DebitAccountRequestModel
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
    }
}