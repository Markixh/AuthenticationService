using AuthenticationService.DAL.Entities;

namespace AuthenticationService.DAL.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User GetByLogin(string login);
    }
}
