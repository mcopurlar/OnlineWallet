namespace MobileRecharge.Core.Services;

public interface IDebitAccountService
{
    Task<DebitAccountResponse> DebitAccount(Guid userId, decimal amount);
}