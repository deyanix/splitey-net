using System.ComponentModel.DataAnnotations.Schema;
using Splitey.Api.Models.User.User;

namespace Splitey.Api.DataModels.Payment;

[Table("SettlementMember", Schema = "payment")]
public class SettlementMemberModel : BaseModel
{
    [ForeignKey(nameof(Settlement))]
    public int SettlementId { get; set; }
    public SettlementModel? Settlement { get; set; }
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public UserModel? User { get; set; }
}