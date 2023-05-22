using Microsoft.AspNetCore.Mvc.Filters;

namespace Bookstore
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action is executing...");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action has executed.");
        }
    }
}
