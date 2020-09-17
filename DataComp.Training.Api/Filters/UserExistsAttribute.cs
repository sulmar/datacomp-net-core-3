using DataComp.Training.IServices;
using DataComp.Training.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Filters
{
    public class UserExistsAttribute : TypeFilterAttribute
    {
        public UserExistsAttribute() : base(typeof(UserExistsFilter))
        {
        }

        private class UserExistsFilter : IAsyncActionFilter
        {
            private readonly IUserService userService;

            public UserExistsFilter(IUserService userService)
            {
                this.userService = userService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                if (!context.ActionArguments.ContainsKey("id"))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                if (!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }

                User user = userService.Get(id);

                if (user==null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }


                await next();

            }
        }
    }
}
