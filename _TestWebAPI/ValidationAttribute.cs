using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationAttribute : Attribute, IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("I am working from end...");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("I am working from start...");
    }

    //public void OnAuthorization(AuthorizationFilterContext context)
    //{
    //    throw new NotImplementedException();
    //}
}
