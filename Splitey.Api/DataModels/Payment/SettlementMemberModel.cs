using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Splitey.Api.Models.User.User;

namespace Splitey.Api.DataModels.Payment;

[Table("SettlementMember", Schema = "settlement")]
public class SettlementMemberModel : IDataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    
    [ForeignKey(nameof(Settlement))]
    public int? SettlementId { get; set; }
    public SettlementModel? Settlement { get; set; }
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public UserModel? User { get; set; }
}