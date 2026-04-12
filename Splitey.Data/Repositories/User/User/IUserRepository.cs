using Splitey.Models.User.User;

namespace Splitey.Data.Repositories.User.User;

public interface IUserRepository
{
    Task<UserDto?> Get(int id);
    Task<UserDto?> GetByLogin(string login);
    Task<IEnumerable<UserItem>> GetList();
}
