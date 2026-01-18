using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Splitey.Api.DataModels.User;

[Table("User", Schema = "user")]
public class UserModel : IDataModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
}