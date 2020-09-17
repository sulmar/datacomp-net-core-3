using DataComp.Training.Api.Events;
using DataComp.Training.IServices;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataComp.Training.Api.Handlers
{
    public class MessageUserHandler : INotificationHandler<UserRemovedEvent>
    {
        private readonly IMessageService messageService;

        public MessageUserHandler(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public Task Handle(UserRemovedEvent @event, CancellationToken cancellationToken)
        {
            messageService.Send($"Usunięto użytkownika {@event.User.FullName}");

            throw new ApplicationException();

            return Task.CompletedTask;
        }
    }
}
