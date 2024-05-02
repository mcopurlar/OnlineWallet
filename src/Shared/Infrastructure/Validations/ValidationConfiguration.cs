namespace OnlineWallet.Infrastructure.Validations;

public class ValidationConfiguration
{
    public const string TopUpValidationRules = "TopUpValidationRules";

    public decimal MaxTopUpTotalAmountPerMonth { get; set; }
    public decimal MaxTopUpTotalAmountByBeneficiaryForVerifiedUser { get; set; }
    public decimal MaxTopUpTotalAmountByBeneficiaryForNotVerifiedUser { get; set; }
    public int MaxActiveBeneficiaryCount { get; set; }

}