using MobileRecharge.Core.Commands;

namespace MobileRecharge.Api.Controllers.Models;

public class AddBeneficiaryRequestModel
{
    public Guid UserId { get; set; }
    public string NickName { get; set; }
    public string MobileNumber { get; set; }

    public AddBeneficiaryCommand ToCommand()
    {
        return new AddBeneficiaryCommand
        {
            UserId = UserId,
            NickName = NickName,
            MobileNumber = MobileNumber
        };
    }
}