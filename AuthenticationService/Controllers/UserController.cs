using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;

        public UserController(ILogger logger) 
        {
            _logger = logger;
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
    }
}
