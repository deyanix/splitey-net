using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitey.Api.DataModels.Payment;

[Table("Settlement", Schema = "settlement")]
public class SettlementModel : IDataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    
    public string? Name { get; set; }
}