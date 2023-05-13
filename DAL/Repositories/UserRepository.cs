using AuthenticationService.DAL.Entities;

namespace AuthenticationService.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "Ivan",
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Email = "g@g.com",
                    Password = "Password",
                    Role = new Role() {
                        Id = 1,
                        Name = "Пользователь"
                    }
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "Petr",
                    FirstName = "Петр",
                    LastName = "Петров",
                    Email = "p@g.com",
                    Password = "Password",
                    Role = new Role() {
                        Id = 1,
                        Name = "Пользователь"
                    }
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "Andrey",
                    FirstName = "Андрей",
                    LastName = "Марков",
                    Email = "a@ya.ru",
                    Password = "Password",
                    Role = new Role() {
                        Id = 2,
                        Name = "Администратор"
                    }
                }
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByLogin(string login)
        {
            return _users.Where(x => x.Login == login).FirstOrDefault();
        }
    }
}
