using Hospital.Domain.Users;

namespace Hospital.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken ct = default);
    }
}
