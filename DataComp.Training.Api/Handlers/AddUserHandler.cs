using DataComp.Training.Api.Requests;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, User>
    {
        private readonly IUserService userService;

        public GetUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(userService.Get(request.Id));
        }
    }

    public class AddUserHandler : IRequestHandler<AddUserRequest, User>
    {
        private readonly IUserService userService;

        public AddUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task<User> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            userService.Add(request.User);

            return Task.FromResult(request.User);
        }
    }
}
