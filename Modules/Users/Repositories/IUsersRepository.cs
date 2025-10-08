using AppApi.Modules.Users.Models;

public interface IUsersRepository
{
    Task<IEnumerable<User>> GetAllAsync();
}