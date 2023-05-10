namespace AuthenticationService
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;

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
                    Password = "Password"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "Petr",
                    FirstName = "Петр",
                    LastName = "Петров",
                    Email = "p@g.com",
                    Password = "Password"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    Login = "Andrey",
                    FirstName = "Андрей",
                    LastName = "Марков",
                    Email = "a@ya.ru",
                    Password = "Password"
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
