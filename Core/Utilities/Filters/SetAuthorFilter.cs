using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Core.Utilities.Filters
{
    public class SetAuthorFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true) return;

            // Get the user ID from JWT claims
            var userIdStr = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId)) return;

            // Loop through action arguments and set AuthorId if the property exists
            foreach (var arg in context.ActionArguments.Values)
            {
                var prop = arg?.GetType().GetProperty("AuthorId");
                if (prop != null && prop.CanWrite)
                {
                    prop.SetValue(arg, userId); // assign integer user ID
                }
            }
        }
    }
}
