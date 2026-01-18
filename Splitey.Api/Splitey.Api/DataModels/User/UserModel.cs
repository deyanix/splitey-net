using System.ComponentModel.DataAnnotations.Schema;

namespace Splitey.Api.DataModels.User;

[Table("User", Schema = "user")]
public class UserModel : BaseModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}