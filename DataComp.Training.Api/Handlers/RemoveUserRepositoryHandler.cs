using DataComp.Training.Api.Events;
using DataComp.Training.IServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Handlers
{

    public class RemoveUserRepositoryHandler : INotificationHandler<UserRemovedEvent>
    {
        private readonly IUserService userService;

        public RemoveUserRepositoryHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public Task Handle(UserRemovedEvent @event, CancellationToken cancellationToken)
        {
            userService.Remove(@event.User.Id);

            return Task.CompletedTask;
        }
    }
}
