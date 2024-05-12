using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Exceptions;

namespace WebAPI.Filter
{
    public class ValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault();
                throw new ValidatorException(errors.ErrorMessage);
            }
        }
    }
}
