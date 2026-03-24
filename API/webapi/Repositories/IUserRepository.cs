using webapi.Models;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task SaveChangesAsync();
}