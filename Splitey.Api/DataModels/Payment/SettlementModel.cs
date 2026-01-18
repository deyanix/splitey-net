using System.ComponentModel.DataAnnotations.Schema;

namespace Splitey.Api.DataModels.Payment;

[Table("Settlement", Schema = "payment")]
public class SettlementModel : BaseModel
{
    public required string Name { get; set; }
}