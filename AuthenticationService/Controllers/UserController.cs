using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

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

        [Authorize]
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
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            User user = _userRepository.GetByLogin(login) ?? throw new AuthenticationException("Пользователь на найден");
            
            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
