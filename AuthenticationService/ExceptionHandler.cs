using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenticationService
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public ExceptionHandler() { }

        public void OnException(ExceptionContext context)
        {
            string message = "Произошла непредвиженная ошибка";

            if (context.Exception is CustomException) 
            { 
                message = context.Exception.Message;
            }

            context.Result = new BadRequestObjectResult(message);
        }
    }
}
