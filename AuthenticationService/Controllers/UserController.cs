using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private IUserRepository _userRepository;

        public UserController(ILogger logger, IMapper mapper, IUserRepository userRepository) 
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
            _logger.WriteEvent("Сообщение о событии в программе");
            _logger.WriteError("Сообщение об ошибке в программе");
        }

        [HttpGet]
        public User GetUser() 
        {
            return new User()
            {
                Id = new Guid(),
                Email = "ya@ya.ru",
                FirstName = "Андрей",
                LastName = "Марков",
                Password = "1112223qwee",
                Login = "Andrey"
            };
        }

        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new()
            {
                Id = new Guid(),
                Email = "ya@ya.ru",
                FirstName = "Андрей",
                LastName = "Марков",
                Password = "1112223qwee",
                Login = "Andrey"
            };

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        [HttpPost]
        [Route("authenticate")]
        public UserViewModel Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = _userRepository.GetByLogin(login);

            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
